using System.Collections;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using TraineeGame;
using System.Collections.Generic;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private Item prefab;

    private FirebaseUser _firebaseUser;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        RegistrationButton.OnUserRegistered += CreateUser;

        LogInButton.OnUserLogedIn += CreateUser;
        UIRegister.OnSilentAuthorization += CreateUser;

        RegistrationButton.OnWriteNewUser += WriteNewUser;
        MainGamePanel.OnLeaderbordOpen += GetDataButton;
        ScoreCount.OnSetScore += WriteNewScore;
    }

    private void CreateUser()
    {
        _firebaseUser = FirebaseAuth.DefaultInstance.CurrentUser;
    }

    private void WriteNewUser(string name)
    {
        DatabaseReference.Instance.Reference.Child("User").Child(_firebaseUser.UserId).Child("name").SetValueAsync(name);
    }

    private void WriteNewScore(int score)
    {
        if (DatabaseReference.Instance == null)
        {
            Debug.Log("Instance is null");
        }

        if(DatabaseReference.Instance.Reference == null)
        {
            Debug.Log("Reference is null");
        }
        if(_firebaseUser == null)
        {
            Debug.Log("ASDADWAED");
        }
        DatabaseReference.Instance.Reference.Child("User").Child(_firebaseUser.UserId).Child("score").SetValueAsync(score);
    }

    public void GetDataButton()
    {
        StartCoroutine(GetUserHighscoreCoroutine());
    }

    private IEnumerator GetUserHighscoreCoroutine()
    {
        var task = DatabaseReference.Instance.Reference.Child("User").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting highscore");
        }
        else
        {
            DataSnapshot snapshot = task.Result;

            
            List<Row> rows = new List<Row>();
            foreach(var item in snapshot.Children)
            {
                if (!item.HasChild("score"))
                    continue;
                
                Row newRow = new Row();
                newRow.Name = item.Child("name").Value.ToString();
                newRow.Score = int.Parse(item.Child("score").Value.ToString());
                rows.Add(newRow);
            }

            ClearLeaderBoard();
            SortRows(rows);

        }

    }

    private void SortRows(List<Row> rowsToSort)
    {
        int placeCount = 1;

        var sortedList = rowsToSort.OrderByDescending(n => n.Score).ToList();

        List<Row> rowsToSortNew = (List<Row>)sortedList;


        for (int i = 0; i < rowsToSort.Count; ++i)
        {
            GameObject rowPosition = GameObject.FindGameObjectWithTag("PrefabPosition");//TODO
            Item row = Instantiate(prefab, rowPosition.transform);

            row.PlaceText.text = placeCount.ToString();
            row.NameText.text = rowsToSortNew[i].Name;

            row.ScoreText.text = rowsToSortNew[i].Score.ToString(); 
            placeCount++;
        }
    }

    private void ClearLeaderBoard()
    {
        GameObject[] itemOnScene = GameObject.FindGameObjectsWithTag("Item");

        foreach(var item in itemOnScene)
        {
            Destroy(item);
        }
    }

    private void OnDestroy()
    {
        RegistrationButton.OnUserRegistered -= CreateUser;
        LogInButton.OnUserLogedIn -= CreateUser;
        UIRegister.OnSilentAuthorization -= CreateUser;

        RegistrationButton.OnWriteNewUser -= WriteNewUser;
        MainGamePanel.OnLeaderbordOpen -= GetDataButton;
        ScoreCount.OnSetScore -= WriteNewScore;
    }
}