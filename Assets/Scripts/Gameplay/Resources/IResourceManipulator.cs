namespace Polyjam_2022
{
    public interface IResourceManipulator : IResourceHolder, IPositionProvider
    {
        public float Range { get; }
    }
}