using System.Collections;
///<summary>
///A structure that holds elements, with a constant length. 
///When a new element is added, the oldest element in the list is replaced
///</summary>
public class ShuffleList<T> : IEnumerable
{
    private T[] shuffleArray;
    private int currentIndex;

    public float length{get;}


    public ShuffleList(int length){
        shuffleArray = new T[length];
        this.length = length;
    }



    public void Add(T element){
        shuffleArray[currentIndex] = element;
        currentIndex ++;
        if(currentIndex >= length)
            currentIndex = 0;
    }

    public T this[int index]
    {
        get { 
            return shuffleArray[index];
        }
    }




    //Enumerator
    public System.Collections.IEnumerator GetEnumerator(){
        return _GetEnumerator();
    }
    private IEnumerator _GetEnumerator(){
        int c = 0;
        while(c < length){
            yield return shuffleArray[c];

            c++;
        }
    }
}
