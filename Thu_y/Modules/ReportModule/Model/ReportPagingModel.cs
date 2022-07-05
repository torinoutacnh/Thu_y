﻿using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ReportPagingModel
    {
        [Range(0,5000)]
        [Required]
        public int PageNumber { get; set; }
        [Range(0, 500)]
        [Required]
        public int PageSize { get; set; }

        public string? Id { get; set; }
        public int? Type { get; set; }
        public string? UserId { get; set; }
        public DateTimeOffset? DateStart { get; set; }
        public DateTimeOffset? DateEnd { get; set; }

        public ReportPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}