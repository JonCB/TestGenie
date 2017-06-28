namespace TestGenie {

    public interface IGeneratorTemplate<T> 
    {
        void Build(BuildContext context, out T subject);
    }

    

}
