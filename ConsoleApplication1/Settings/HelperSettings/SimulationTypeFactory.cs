using System.Linq;
using ParticleSimulation.Initialization;
using ParticleSimulation.Tactics.AgreedTransmission;
using ParticleSimulation.Tactics.AgreedTransmission.Sorting;
using ParticleSimulation.Tactics.GNeighbour;
using ParticleSimulation.Tactics.Helsim;
using ParticleSimulation.Tactics.KMeans;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSorting;
using ParticleSimulation.Tactics.MatrixSort;
using ParticleSimulation.Tactics.OuterList;
using ParticleSimulation.Tactics.SideSort;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization;

namespace ParticleSimulation.Settings.HelperSettings
{
    public class SimulationTypeFactory
    {
        public void SetMatrixSettings(InitializationParameters retPar, int timesSorting,int perTimeSorting, int cellSize)
        {
            retPar.CoreFactory = new MatrixSortCoreFactory();
            retPar.SectionFactory = new MatrixSortSectionFactory(cellSize);
            retPar.SorterFactory = new MatrixSorterFactory(timesSorting,perTimeSorting);
            retPar.ParticlesPerCell = cellSize*cellSize*cellSize;
        }
   

        public void SetMatrixSettings(InitializationParameters retPar, int timesSorting,int cellSize)
        {
            SetMatrixSettings(retPar,timesSorting,1,cellSize);    
        }
        public void SetHelsimSettings(InitializationParameters retPar)
        {
            retPar.CoreFactory = new HelsimCoreFactory();
            retPar.SectionFactory = new HelsimSectionFactory();
            retPar.SorterFactory = new HelsimSorterFactory();
        }

        public void SetStrictBoundarySettings(InitializationParameters retPar)
        {
            retPar.CoreFactory = new StrictBoundaryCoreFactory();
            retPar.SectionFactory = new StrictBoundarySectionFactory();
            retPar.SorterFactory = new StrictBoundarySorterFactory();
        }

        public void SetKMeansSettings(InitializationParameters retPar)
        {
            retPar.CoreFactory = new KMeansCoreFactory();
            retPar.SectionFactory = new KMeansSectionFactory(0.0f, 0.8f, 1f);
            retPar.SorterFactory = new SingleDimKMeansSorterFactory(0.1f);
            retPar.VisualizationParameters.ParticleSubVisualizers.Add(
                new NtSectionCenterVisualizer(retPar.VisualizationParameters.PictureCoordinateConverter));
        }

        public void SetGNeighbourSettings(InitializationParameters retPar)
        {
            retPar.CoreFactory = new GNeighbourCoreFactory();
            retPar.SorterFactory = new GNeighbourSorterFactory(0.1f);
            if (!retPar.VisualizationParameters.ParticleSubVisualizers.Any(o => o is NtSectionCenterVisualizer))
                retPar.VisualizationParameters.ParticleSubVisualizers.Add(
                    new NtSectionCenterVisualizer(retPar.VisualizationParameters.PictureCoordinateConverter));
            retPar.SectionFactory = new GNeighbourSectionFactory(0.1f);
        }

        public void SetBcSettings(InitializationParameters retPar)
        {
            retPar.CoreFactory = new FtCoreFactory();
            retPar.SorterFactory = new BcSorterFactory();
            retPar.SectionFactory = new FtSectionFactory(0.1f*0.1f, 2);
        }

        public void SetSideSortSettings(InitializationParameters retPar, int initialElementsPerSide)
        {
            retPar.CoreFactory = new SideSortCoreFactory();
            retPar.SorterFactory = new SideSorterFactory();
            retPar.SectionFactory = new SideSorterSectionFactory(initialElementsPerSide);
        }
    }
}