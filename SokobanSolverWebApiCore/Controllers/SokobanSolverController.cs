using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sokoban.DataTypes;
using Sokoban.SokobanSolvingLogic;
using System.Text.Json;

namespace SokobanSolverWebApiCore.Controllers
{
    [ApiController]
    public class SokobanSolverController : ControllerBase
    {
        [HttpPost]
        [Route("Solve")]
        [EnableCors]
        public async Task<Sokoban.DataTypes.Path> Solve([FromBody] List<SokobanObject> levelObjects, CancellationToken cancellationToken)
        {
            HeuristicsSolver solver = new HeuristicsSolver();
            return await Task.Run(() => solver.Solve(levelObjects, cancellationToken), cancellationToken);
        }
    }
}
