using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Mafia.Droid
{
	[Activity (Label = "Mafia.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        public static Boolean globalFlag;
		int count = 1;
        int[] idArray = new int[15];
        int[] idArray2 = new int[15];
        Player[] playerArray = new Player[15];
        Role[] roleArray = new Role[15];

        public static int[] saveArray = new int[15];
        public static int[] saveArray2 = new int[15];
        public static Player[] saveArray3 = new Player[15];
        public static Role[] saveArray4 = new Role[15];

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if(globalFlag == true)
            {
                idArray = saveArray;
                idArray2 = saveArray2;
                playerArray = saveArray3;
                roleArray = saveArray4;
            }
            globalFlag = false;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Button newGame = FindViewById<Button>(Resource.Id.button1);
            Button addRole = FindViewById<Button>(Resource.Id.button2);
            Button newPlayer = FindViewById<Button>(Resource.Id.button3);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            newGame.Click += delegate {
                int counter = 0;
                for (int i = 0; i < idArray.Length; i++)
                {
                    if (idArray[i] != 0)
                    {
                        counter++;
                    }
                }
                int counter2 = 0;
                for (int i = 0; i < playerArray.Length; i++)
                {
                    if (playerArray[i] != null)
                    {
                        counter2++;
                    }
                }
                int counter3 = 0;
                for (int i = 0; i < roleArray.Length; i++)
                {
                    if (roleArray[i] != null)
                    {
                        counter3++;
                    }
                }
                if (counter < 7)
                {
                    Toast.MakeText(this, "Not enough players", ToastLength.Short).Show();
                    SetContentView(Resource.Layout.Main);
                    MainActivity main = new MainActivity();
                }
                else if (counter < 10)
                {
                    Boolean flagDone = false;
                    while (flagDone == false)
                    {
                        Boolean allAssigned = true;
                        Random r = new Random();
                        int rInt = r.Next(0, counter);
                        Random r2 = new Random();
                        int rInt2 = r.Next(0, counter3);
                        if (roleArray[rInt2].getTaken() == false & playerArray[rInt].getRole() == null)
                        {
                            playerArray[rInt].setRole(roleArray[rInt2]);
                            roleArray[rInt2].setTaken(true);
                        }
                        for (int i = 0; i < counter2; i++)
                        {
                            if (playerArray[i].getRole() == null)
                            {
                                allAssigned = false;
                            }
                            if (allAssigned == true)
                            {
                                flagDone = true;
                            }
                        }
                    }
                    //GAME READY ALL PLAYERS HAVE ROLES
                    saveArray = idArray;
                    saveArray2 = idArray2;
                    saveArray3 = playerArray;
                    saveArray4 = roleArray;
                    globalFlag = true;
                    Console.WriteLine("DONE");
                    Toast.MakeText(this, "Ready to Play", ToastLength.Short).Show();
                }
                StartActivity(typeof(MainActivity));
            };
            addRole.Click += delegate { SetContentView(Resource.Layout.Role);
                SetContentView(Resource.Layout.Role);
                EditText num = FindViewById<EditText>(Resource.Id.numPlayers2);
                EditText p1 = FindViewById<EditText>(Resource.Id.player2);
                idArray2[0] = p1.Id;
                Button submit = FindViewById<Button>(Resource.Id.submit2);
                Button add = FindViewById<Button>(Resource.Id.add2);
                int size = 0;
                for (int i = 0; i < idArray.Length; i++)
                {
                    if (idArray[i] != 0)
                    {
                        size++;
                    }
                }
                int number = (size-1);
                RadioGroup rg = FindViewById<RadioGroup>(Resource.Id.radioGroup4);
                int counter2 = 0;
                for (int i = 0; i < idArray2.Length; i++)
                {
                    if (idArray2[i] != 0)
                    {
                        counter2++;
                    }
                }
                if (counter2 + number < 14)
                {
                    for (int i = 0; i < number; i++)
                    {
                        EditText p = new EditText(this);
                        p.SetWidth(rg.Width);
                        int id = rg.GetChildAt(i).Id;
                        p.Id = id + 10;
                        rg.AddView(p);
                        if (idArray2[i + 1] == 0)
                        {
                            idArray2[i + 1] = p.Id;
                        }
                        else
                        {
                            idArray2[i + counter2] = p.Id;
                        }
                    }
                }
                submit.Click += delegate
                {
                    int counter = 0;
                    for (int i = 0; i < idArray2.Length; i++)
                    {
                        if (idArray2[i] != 0)
                        {
                            counter++;
                        }
                    }
                    for (int i = 0; i < counter; i++)
                    {
                        EditText b = FindViewById<EditText>(idArray2[i]);
                        String n = b.Text.ToString();
                        Role role = new Role();
                        role.setName(n);
                        if (n.Equals("mafia") | n.Equals("Mafia") | n.Equals("vigilante") | n.Equals("Vigilante") | n.Equals("arsonist") | n.Equals("Arsonist"))
                        {
                            role.setKillpower(true);
                            role.setTargets(1);
                            role.setGoodness(true);
                            if (n.Equals("mafia") | n.Equals("Mafia"))
                            {
                                role.setGoodness(false);
                            }
                        }
                        else
                        {
                            role.setKillpower(false);
                            role.setGoodness(true);
                            if (n.Equals("cop") | n.Equals("Cop") | n.Equals("doctor") | n.Equals("Doctor"))
                            {
                                role.setTargets(1);
                            }
                            else if (n.Equals("town") | n.Equals("Town") | n.Equals("amnesiac") | n.Equals("Amnesiac"))
                            {
                                role.setTargets(0);
                            }
                            else if (n.Equals("transporter") | n.Equals("Transporter"))
                            {
                                role.setTargets(2);
                            }
                        }
                        roleArray[i] = role;
                    }
                    saveArray = idArray;
                    saveArray2 = idArray2;
                    saveArray3 = playerArray;
                    saveArray4 = roleArray;
                    globalFlag = true;
                    SetContentView(Resource.Layout.Main);
                    StartActivity(typeof(MainActivity));
                };
            };
            newPlayer.Click += delegate {
                SetContentView(Resource.Layout.Player);
                EditText num = FindViewById<EditText>(Resource.Id.numPlayers);
                EditText p1 = FindViewById<EditText>(Resource.Id.player1);
                idArray[0] = p1.Id;
                Button submit = FindViewById<Button>(Resource.Id.submit);
                Button add = FindViewById<Button>(Resource.Id.add);
                add.Click += delegate
                {
                    int number = Int32.Parse(num.EditableText.ToString());
                    RadioGroup rg = FindViewById<RadioGroup>(Resource.Id.radioGroup2);
                    int counter = 0;
                    for (int i = 0; i < idArray.Length; i++)
                    {
                        if (idArray[i] != 0)
                        {
                            counter++;
                        }
                    }
                    if (counter + number < 14)
                    {
                        for (int i = 0; i < number; i++)
                        {
                            EditText p = new EditText(this);
                            p.SetWidth(rg.Width);
                            int id = rg.GetChildAt(i).Id;
                            p.Id = id + 10;
                            rg.AddView(p);
                            if (idArray[i + 1] == 0)
                            {
                                idArray[i + 1] = p.Id;
                            }
                            else
                            {
                                idArray[i + counter] = p.Id;
                            }
                        }
                    }
                };
                submit.Click += delegate
                {
                    int counter = 0;
                    for (int i = 0; i < idArray.Length; i++)
                    {
                        if (idArray[i] != 0)
                        {
                            counter++;
                        }
                    }
                    for (int i = 0; i < counter; i++)
                    {
                        EditText b = FindViewById<EditText>(idArray[i]);
                        String n = b.Text.ToString();
                        Player player = new Player();
                        player.setName(n);
                        playerArray[i] = player;
                    }
                    saveArray = idArray;
                    saveArray2 = idArray2;
                    saveArray3 = playerArray;
                    saveArray4 = roleArray;
                    globalFlag = true;
                    SetContentView(Resource.Layout.Main);
                    StartActivity(typeof(MainActivity));
                };
            };
        }
	}
}


