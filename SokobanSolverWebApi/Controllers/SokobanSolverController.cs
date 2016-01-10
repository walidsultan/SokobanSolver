using Sokoban.DataTypes;
using Sokoban.SokobanSolvingLogic;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SokobanSolverWebApi.Controllers
{
    public class SokobanSolverController : ApiController
    {

        [HttpPost]
        [Route("Solve")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Path> Solve(List<SokobanObject> levelObjects, CancellationToken cancellationToken)
        {
            HeuristicsSolver solver = new HeuristicsSolver();
            return await Task.Run(() => solver.Solve(levelObjects, cancellationToken), cancellationToken);
        }

    }
}
