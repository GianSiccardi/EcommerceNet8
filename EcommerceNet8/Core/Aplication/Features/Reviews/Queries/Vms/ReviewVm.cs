﻿namespace EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms
{
    public class ReviewVm
    {

        public  int Id { get; set; }

        public  string? Name { get; set; }


        public string? Comment { get; set; }
        public string? Rating { get; set; }

        public int ProductId { get; set; }

    }
}
