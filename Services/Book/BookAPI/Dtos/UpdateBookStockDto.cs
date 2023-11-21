namespace BookAPI.Dtos
{
    public class UpdateBookStockDto
    {
        public string BookId { get; set; }
        public int NewQuantity { get; set; }
    }
}
