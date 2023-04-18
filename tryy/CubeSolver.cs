using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace tryy
{

    public class CubeSolver
    {
        const int max_moves =1;

        public IDictionary<string, int> heuristic { get; set; }
        public IDictionary<string, int> fromJson { get; set; }
        public Queue<PosibbleStatus> que { get; set; }
        public Cube myCube { get; set; }
        public PosibbleStatus SD { get; set; }
        public string a_str { get; set; }
        public bool status;
        public List<ActionInCube> moves;
        public int max_depth = max_moves;
        public int min_val;
        public int threshold = max_moves;
        public int min_threshold = 0;
        public BestAction best_action;
        public int h_score;
        public int f_score;
        public string text { get; set; }
        
        public CubeSolver()
        {
            myCube = new Cube();
            moves = new List<ActionInCube>();
            //fromJson = new Dictionary<string, int>();
            //text = File.ReadAllText(Path.Combine(Path.GetDirectoryName(
            //    Assembly.GetExecutingAssembly().Location), "heuristic.json"));
            //fromJson = JsonSerializer.Deserialize<Dictionary<string, int>>(text);
            heuristic = new Dictionary<string, int>();//המילון שמכיל את כל המצבים האפשריים לפתרון הקוביה
            status = true;
        }

        public void build_heuristic_db(Cube c1,string state, List<ActionInCube> actions)
        {
            //state-מחרוזת המתארת את מצב הקוביה
            //actions- רשימה בגודל 18 שמתארת את כל הפעולות האפשריות
            SD = new PosibbleStatus(state, 0,c1);
            heuristic = new Dictionary<string, int>();
            heuristic.Add(state, 0);
            que = new Queue<PosibbleStatus>();
            que.Enqueue(SD);
            while (que.Count > 0)
            {
                SD = que.Dequeue();
                if (SD.d > max_moves)
                    continue;
                for (int i = 0; i < actions.Count; i++)
                {
                    myCube = new Cube();
                    myCubeEquleC1(SD.c);
                    // myCube.state = SD.state;
                    if (actions[i].actType == eActType.horizontal)
                        myCube.horizontal_twist(actions[i].direction, actions[i].index);
                    else if (actions[i].actType == eActType.vertical)
                        myCube.vertical_twist(actions[i].direction, actions[i].index);
                    else if (actions[i].actType == eActType.side)
                        myCube.side_twist(actions[i].direction, actions[i].index);
                    a_str = myCube.matToStr();
                    try
                    {
                        if (!(heuristic.ContainsKey(a_str)))
                            heuristic.Add(a_str, SD.d + 1);
                       else if(heuristic.FirstOrDefault(x => x.Key == a_str).Value > SD.d + 1) 
                            heuristic[a_str] = SD.d + 1;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        createJsonFile();
                        return;
                    }
                    que.Enqueue(new PosibbleStatus(a_str, SD.d + 1,myCube));
                   
                }
               
            }
            createJsonFile();
        }
        public void createJsonFile()
        {
            string json = JsonSerializer.Serialize(heuristic);
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location), "heuristic.json"), json);
        }

        public List<ActionInCube> run(Cube myCube, List<ActionInCube> actions)
        {
            while (status)
            {
                status = search(myCube, 1, actions);
                if (status)
                {
                    for (int i = 0; i < moves.Count; i++)
                    {
                        Console.WriteLine(moves[i]);
                    }
                   
                    return moves;
                }
                moves = new List<ActionInCube>();
               threshold = min_threshold;
          
            }
            return null;
        }

        public void myCubeEquleC1(Cube c1)
        {
            for (int d = 0; d < 3; d++)
                for (int y = 0; y < 3; y++)
                {
                    myCube.Up[d, y] = c1.Up[d, y];
                    myCube.Down[d, y] = c1.Down[d, y];
                    myCube.Left[d, y] = c1.Left[d, y];
                    myCube.Right[d, y] = c1.Right[d, y];
                    myCube.Front[d, y] = c1.Front[d, y];
                    myCube.Back[d, y] = c1.Back[d, y];
                }
        }
        public bool search(Cube c1, int g_score, List<ActionInCube> actions)
        {
            myCube = c1;
            myCube.state = myCube.matToStr();
            if (myCube.isSolvedCube(myCube.state))
                return true;
            else if (moves.Count > threshold)
                return false;
            min_val =0;
            best_action = null;
            for (int i = 0; i < actions.Count; i++)
            {
                myCube = new Cube();
                myCubeEquleC1(c1);
                if (actions[i].actType == eActType.horizontal)
                    myCube.horizontal_twist(actions[i].direction, actions[i].index);
                else if (actions[i].actType == eActType.vertical)
                    myCube.vertical_twist(actions[i].direction, actions[i].index);
                else if (actions[i].actType == eActType.side)
                    myCube.side_twist(actions[i].direction, actions[i].index);
                a_str = myCube.matToStr();
                myCube.state = a_str;
                 if (myCube.isSolvedCube(a_str))
                {
                    moves.Add(actions[i]);
                    return true;
                }
                if (fromJson.Keys.Contains(a_str))
                {
                    h_score = fromJson.FirstOrDefault(x => x.Key == a_str).Value;
                    //moves.Add(actions[i]);
                }
                else
                    h_score = max_depth;
                f_score = g_score + h_score;
                if (f_score < min_val)
                {
                    min_val = f_score;
                    best_action = new BestAction(a_str, actions[i]);
                }
            }
                if (best_action != null)
                {
                    if (min_threshold == 0 || min_val < min_threshold)
                        min_threshold = min_val;
                    //moves.Add(actions[index]);
                    status = search(myCube, g_score, actions);
                    if (status)
                    {
                        return true;
                    }
                }
                
            return false;
        }
    }
}
      
