using AutoMapper;
using BookstoreService.Application.Interface;
using BookstoreService.Application.Models;
using BookstoreService.Domain.Entities;
using BookstoreService.Infrastructure.Repositories;
using Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreService.Application.Service
{
    public class BookstoreService : IBookstoreService
    {
        private readonly BookstoreRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public BookstoreService(BookstoreRepository repo, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<PagedResult<Bookstore>> GetAllAsync(int pageNo, int pageSize)
        {
            var bsL = await _repo.GetAllAsync();
            return PagedResult<Bookstore>.Create(bsL, pageNo, pageSize);
        }

        public async Task<Bookstore> CreateAsync(BookstoreCreateRequest request)
        {
            var entity = _mapper.Map<Bookstore>(request);

            // Upload image nếu có
            if (request.ImageFile != null)
            {
                entity.ImageUrl = await _cloudinaryService.UploadImageAsync(request.ImageFile);
            }

            entity.OwnerId = request.OwnerId;
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;

            return await _repo.CreateAsync(entity);
        }

        public async Task<Bookstore> UpdateAsync(BookstoreUpdateRequest request)
        {
            var existEntity = await _repo.GetByIdAsync(request.Id);
            if (existEntity == null)
            {
                throw new Exception("Bookstore not found");
            }

            _mapper.Map(request, existEntity);

            // Upload ảnh mới nếu có
            if (request.ImageFile != null)
            {
                existEntity.ImageUrl = await _cloudinaryService.UploadImageAsync(request.ImageFile);
            }

            existEntity.UpdatedAt = DateTime.Now;

            return await _repo.UpdateAsync(existEntity);
        }

        public async Task<bool> Ban(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
                return await _repo.Ban(id);
            throw new Exception("Bookstore not found");
        }

        public async Task<Bookstore> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id) ?? throw new Exception("Bookstore not found");
        }
        public async Task<Bookstore> GetByOwnerId(string ownerId)
        {
            return await _repo.GetByOwnerId(ownerId);
        }
    }
}
