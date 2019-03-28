using CartridgesNS.ViewModel;

namespace CartridgesNS.Other
{
    public interface IContentChanger
    {
        void ChangeContent(BaseViewModel sender, BaseViewModel newContent);
    }
}
