public interface ILayout 
{
    void SetLayout(int height, MapSetter indoorSetter);
    ILayout SetParams();
    int ReturnWidth();
    int ReturnBreadth();
}
