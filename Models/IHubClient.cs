using System.Threading.Tasks;

namespace Novi.Models
{
    public interface IHubClient
    {
        Task BroadcastMessage(Product product, Auction auction);
    }
}