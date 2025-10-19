using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreService.Application.Models
{
    public class BookstoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; } // URL ảnh từ Cloudinary
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class BookstoreCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

    public class BookstoreCreateDTO
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; } // URL đã upload
    }
    public class BookstoreUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

    public class BookstoreUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
    }


}
