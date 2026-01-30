using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Ohara.API.Shared.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public required string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}