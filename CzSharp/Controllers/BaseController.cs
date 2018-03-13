using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    /// <summary>
    /// Parent controller of all main area controllers
    /// For future usage of shared methods
    /// </summary>
    public abstract class BaseController: Controller
    {
    }
}