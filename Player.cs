public class Player {
 private string _name;
 private int _score;
 private int _lives;
 public Player(string name, int lives) {
    _name = name;
    _lives = lives;
 }
 public int score() => _score;
 public void decScore(int points) => _score -= points;
 public void incScore (int points) => _score += points;
 public void decLives() => _lives --;
 public void incLives() => _lives ++;
}