using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models
{
    /// <summary>
    /// Input chat model
    /// </summary>
    public class ChatModel
    {
        /// <summary>
        /// Message text
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}
