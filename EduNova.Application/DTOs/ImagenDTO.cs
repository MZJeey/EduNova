namespace EduNova.Application.DTOs
{
    public class ImagenDTO
    {
        public int IdImagen { get; set; }
        public string Url { get; set; } = null!;
        public int IdTicket { get; set; }
    }
}