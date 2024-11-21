using System.Collections.Generic;
using Rca.Models;

namespace Rca.Services
{
    public interface ICauseService
    {
        /// <summary>
        /// Gets a list of classifications.
        /// </summary>
        /// <returns>List of classifications.</returns>
        IEnumerable<ClassificationModel> GetClassifications();

        /// <summary>
        /// Gets the cause details for a provider cause id.
        /// </summary>
        /// <param name="causeId">The cause identifier.</param>
        /// <returns>Cause model.</returns>
        CauseModel GetCauseById(int causeId);

        /// <summary>
        /// Creates a cause.
        /// </summary>
        /// <param name="rcaId">The rca identifier.</param>
        /// <param name="causeModel">Cause model.</param>
        void CreateCause(int rcaId, CauseModel causeModel);

        /// <summary>
        /// Gets a list of RCA classification causes and it's sub-causes.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>Cause select list model.</returns>
        List<CauseSelectListModel> GetRCAClassificationCauses(int rcaId, int classificationId);

        /// <summary>
        /// Gets a list of classifications for a provided RCA id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>List of classifications.</returns>
        IEnumerable<ClassificationModel> GetClassificationsByRCAId(int rcaId);
    }
}
