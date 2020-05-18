using System.Threading.Tasks;
using Template.Domain.V1.Models.Request;

namespace Template.Domain.V1.Interfaces
{
    /// <summary>
    /// Removes a layer of abstraction by defining what TOutput actually is
    /// </summary>
    public interface IExample<T>
    {
        /// <summary>
        /// Example GET
        /// </summary>
        /// <param name="model">Example model</param>
        /// <returns>Example result</returns>
        Task<T> Get(Example model);

        /// <summary>
        /// Example POST
        /// </summary>
        /// <param name="model">Example model</param>
        /// <returns>Example result</returns>
        Task<T> Post(Example model);
    }
}