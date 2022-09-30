using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace SVTAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RobotsController : ControllerBase
{ 
    static HttpClient client = new HttpClient();
    private readonly ILogger<RobotsController> _logger;
    private readonly IConfiguration _Config;
    public RobotsController(ILogger<RobotsController> logger, IConfiguration pConfig)
    {
        _logger = logger;
        _Config = pConfig;
    }

    async Task<IEnumerable<Bot>> GetBotData()
    {

        return await client.GetFromJsonAsync<IEnumerable<Bot>>(_Config.GetValue<string>("RobotListApiUrl")); 
    }

    [HttpPost(Name="closest")]
    public JsonResult closest([FromBody] LocationQuery query)
    { 
        IEnumerable<Bot> bots = GetBotData().Result; 

        IEnumerable<Bot> filteredBots = bots.Where(o=>Math.Sqrt(Math.Pow(o.x-query.x, 2) + Math.Pow(o.y-query.y,2))<=10 ).OrderByDescending(o=>o.batteryLevel).ToArray();
        Bot final = filteredBots.FirstOrDefault();

        if(final == null)
        {
            return new JsonResult(new {
            error="An error has ocurred",
            x = query.x,
            y = query.y
            });
        }

        return new JsonResult(new {
            robotId = final.robotId,
            distanceToGoal = Math.Round(Math.Sqrt(Math.Pow(final.x-query.x, 2) + Math.Pow(final.y-query.y,2)),3),
            batteryLevel = final.batteryLevel
        });
    }
}
