﻿namespace BookAPI.Dtos
{
    public class CreateBookDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
