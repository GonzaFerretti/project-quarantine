public class ActionEnterFlingMode : IAction
{
    public void Do(Model m)
    {
        ModelPlayable mp = m as ModelPlayable;
        FlingSpotLight fs = mp.flingSpotlight;

        if (mp.controller != mp.flingController)
        {
            fs.gameObject.SetActive(true);
            mp.controller = mp.flingController;
        }
        else
        {
            fs.gameObject.SetActive(false);
            mp.controller = mp.usualController;
        }
    }
}
