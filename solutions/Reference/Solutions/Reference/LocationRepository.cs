using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    public class LocationRepository : ILocationRepository, IReverseLocationRepository
    {
        private readonly Dictionary<IReagent, ITrough> _reagentLocations = new Dictionary<IReagent, ITrough>();
        private readonly Dictionary<ISample, (IMicroplate, int)> _sampleProcessingLocations = new Dictionary<ISample, (IMicroplate, int)>();
        private readonly Dictionary<ISample, (ITubeRunner, int)> _sampleOrigins = new Dictionary<ISample, (ITubeRunner, int)>();

        private readonly Dictionary<(ILabware, int), ISample> _sampleTracker = new Dictionary<(ILabware, int), ISample>();

        public void SetOrigin(IReagent reagent, ITrough trough)
        {
            _reagentLocations.Add( reagent, trough );
        }

        public void SetProcessingLocation(ISample sample, IMicroplate microplate, int cavity)
        {
            _sampleProcessingLocations.Add( sample, (microplate, cavity) );
            _sampleTracker.Add( (microplate, cavity), sample );
        }

        public void SetOrigin(ISample sample, ITubeRunner tubeRunner, int cavity)
        {
            _sampleOrigins.Add( sample, (tubeRunner, cavity) );
            _sampleTracker.Add( (tubeRunner, cavity), sample );
        }

        public ITrough LocateReagent( IReagent reagent )
        {
            return _reagentLocations[reagent];
        }

        public (ITubeRunner TubeRunner, int Cavity) LocateSampleOrigin( ISample sample )
        {
            return _sampleOrigins[sample];
        }

        public (IMicroplate Plate, int Cavity) LocateSampleProcessing( ISample sample )
        {
            return _sampleProcessingLocations[sample];
        }

        public ISample IdentifySample( ILabware labware, int cavity )
        {
            if (_sampleTracker.TryGetValue((labware, cavity), out var sample))
            {
                return sample;
            }
            return null;
        }
    }
}
