using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
	private Game game;


    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    // 1
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 4
        float initialYPos = asteroid.transform.position.y;
        // 5
        yield return new WaitForSeconds(0.1f);
        // 6
        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator ShipMovesLeft()
    {
        Ship player = game.GetShip();

        float initialpos = player.transform.position.x;

        player.MoveLeft();

        yield return new WaitForSeconds(0.1f);

        Assert.AreNotEqual(player.transform.position.x, initialpos);
    }

    [UnityTest]
    public IEnumerator ShipMovesRight()
    {
        Ship player = game.GetShip();

        float initialpos = player.transform.position.x;

        player.MoveRight();

        yield return new WaitForSeconds(0.1f);

        Assert.AreNotEqual(player.transform.position.x, initialpos);
    }


    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //1
        game.isGameOver = true;
        game.NewGame();
        //2
        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
        
    }


    [UnityTest]
    public IEnumerator scoreResetTest()
    {
        //1
        game.score = 10;
        game.NewGame();
        //2

        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(game.score, 0);


    }



    [UnityTest]
    public IEnumerator gameScoreWinConditionTest()
    {
        //1
        game.score = 20;
        //2

        yield return new WaitForSeconds(0.3f);
        Assert.True(game.isGameWon);
    }



    [UnityTest]
    public IEnumerator NewGameAfterWin()
    {
        //1
        game.isGameWon = true;
        game.NewGame();
        //2
        Assert.False(game.isGameWon);
        yield return null;
    }

    //[UnityTest]
    //public IEnumerator CloseGameAfterWin()
    //{
    //    //1
    //    game.isGameWon = true;
    //    //2

    //    Assert.False(UnityEditor.EditorApplication.isPlaying);
    //    yield return null;
    //}
}