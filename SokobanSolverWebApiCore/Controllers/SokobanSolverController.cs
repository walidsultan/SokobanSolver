using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sokoban.DataTypes;
using Sokoban.SokobanSolvingLogic;

namespace SokobanSolverWebApiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SokobanSolverController : ControllerBase
    {
        [HttpPost]
        [Route("Solve")]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Sokoban.DataTypes.Path> Solve(List<SokobanObject> levelObjects, CancellationToken cancellationToken)
        {
            HeuristicsSolver solver = new HeuristicsSolver();
            return await Task.Run(() => solver.Solve(levelObjects, cancellationToken), cancellationToken);
        }
    }
}
