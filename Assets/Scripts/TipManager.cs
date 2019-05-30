using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour {

	public static TipManager Instance = null;
	public static Animator anim;

	public Text messageText;

	//To avoid seing the same tip over and over and over again
	private static int counterMagma = 25;
	private static int counterIce = 25;
	private static int counterLadder = 50;
	private static int counterAcid = 15;

    //ZozzyFirstLevel
    private static int counterSaveMe = 10;
    private static int counterDoorExplosion = 50;
    private static int counterTimNarcisist = 10;
    private static int counterWtf = 100;
    private static int counterRedKey = 50;
    private static int counterBlueKey = 100;
    private static int counterHolyLight = 5;
    private static int counterUnlocking1 = 10;
    private static int counterExplainingDetonation = 10;
    private static int counterCoolGuys = 5;
    private static int counterTnt = 50;
    private static int counterKarmaCoin = 100;
    private static int counterCorrectKey = 2;
    private static int counterExplainButtonsOpenUse = 25;
	private static int counterTntPlaced = 5;
	private static int counterExplainAllButtons = 5;
    private static int counterPlayerDead = 1;

    private static int counterGameSewer = 15;
	private static int counterLift = 5;
	private static int counterFlame = 5;
	private static int counterMiB = 10;
	private static int counterPlatform = 10;
	private static int counterCoinRoom = 10;
	private static int counterMiB2 = 5;
	private static int counterKey = 5;
	private static int counterLaser = 5;
	private static int counterPress = 15;
	private static int counterHoly = 15;
	private static int counterYellowKey = 15;
	private static int counterLaberint = 10;
	private static int counterPassword = 10;
	private static int counterGreenKey = 10;
	private static int counterRoofL = 10;
	private static int counterRoofR = 10;
	private static int counterComputer1 = 5;
	private static int counterComputer2 = 5;
	private static int counterLaberintL = 5;
	private static int counterHint1 = 25;
	private static int counterHint2 = 25;
	private static int counterHint2b = 25;
	private static int counterHint3 = 25;
	private static int counterHint4 = 25;
	private static int counterHint5 = 25;
	private static int counterHint6 = 25;
	private static int counterHint7 = 25;
	private static int counterHint8 = 25;
    

    void Awake() {
		if ( Instance == null ) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public static void sendMessage (string messageName) {
		Instance.Invoke (messageName, 0.01f);
		resetCounters ();
	}

	void ladder() {
		if ( counterLadder >= 50 ) {
			setContent("There's a ladder to the left you can use to enter the palace!!!");
		}
		counterLadder--;
	}

	void acid() {
		if ( counterAcid >= 15 ) {
			setContent("Watch out! Watch out! Acid will make you weaker. Use your double jump to avoid it!!!");
		}
		counterAcid--;
	}

	void magma() {
		if ( counterMagma >= 25 ) {
			setContent("Hey! That's magma, is even hotter than me!!!");
		}
		counterMagma--;
	}

	void ice() {
		if ( counterIce >= 25 ) {
			setContent("That's crystal, be fast, or it'll break even faster than your last relationship!!!");
		}
		counterIce--;
	}

	void gameSewer() {
		if ( counterGameSewer >= 15 ) {
			setContent("Hurry Foxy Ninja, use the ladder and go up up up!");
		}
		counterGameSewer--;
	}

	void lift() {
		if ( counterLift >= 5 ) {
			setContent("Use the elevator pal, just press the Up arrow or W!");
		}
		counterLift--;
	}

	void flame() {
		if ( counterFlame >= 5 ) {
			setContent("Ouch! This is getting hotter and hotter, you'll burn kiddo!");
		}
		counterFlame--;
	}

	void mib() {
		if ( counterMiB >= 10 ) {
			setContent("Enemies will only see you under the light. Kill them softly with your knife using H!!");
		}
		counterMiB--;
	}

	void platform() {
		if ( counterPlatform >= 10 ) {
			setContent("Move move move! Jump to that platform and move!");
		}
		counterPlatform--;
	}

	void coinRoom() {
		if ( counterCoinRoom >= 10 ) {
			setContent("Feeling tired huh? Enter this room and get some rest!");
		}
		counterCoinRoom--;
	}

	void mib2() {
		if ( counterMiB2 >= 5 ) {
			setContent("They're getting even stronger! Use your invisibility power with J");
		}
		counterMiB2--;
	}

	void purpleKey() {
		if ( counterKey >= 5 ) {
			setContent("You need a purple key to open this door! Kill whoever has it pal!");
		}
		counterKey--;
	}

	void laser() {
		if ( counterLaser >= 5 ) {
			setContent("Watch out watch out! These lasers are a serious deal!");
		}
		counterLaser--;
	}

	void press() {
		if ( counterPress >= 15 ) {
			setContent("That looks heavy my friend! Be fast and careful!");
		}
		counterPress--;
	}

	void greenKey() {
		if ( counterGreenKey >= 15 ) {
			setContent("You need a green key to open this door! Kill whoever has it pal!");
		}
		counterGreenKey--;
	}

	void holy() {
		if ( counterHoly >= 15 ) {
			setContent("These yellow lights are not inofensive at all, don't saty too long or you will die!");
		}
		counterHoly--;
	}

	void yellowKey() {
		if ( counterYellowKey >= 15 ) {
			setContent("You need a yellow key to open this door! Go to the room at your right and grab it!!");
		}
		counterYellowKey--;
	}

	void laberint() {
		if ( counterLaberint >= 5 ) {
			setContent("Use the platforms to collect 2 little notes that contain the password you need to escape!");
		}
		counterLaberint--;
	}

	void laberintL() {
		if ( counterLaberintL >= 5 ) {
			setContent("You can use the doors to collect the passwords, but it makes you weaker!");
		}
		counterLaberintL--;
	}

	void hint1() {
		if ( counterHint1 >= 25 ) {
			setContent("Use this platform to reach Tim's office, maybe there's something there");
		}
		counterHint1--;
	}

	void hint2() {
		if ( counterHint2 >= 25 ) {
			setContent("You found a piece of the password. Good boy!");
		}
		counterHint2--;
	}

	void hint2b() {
		if ( counterHint2b >= 25 ) {
			setContent("You found a piece of the password. Good boy!");
		}
		counterHint2b--;
	}

	void hint3() {
		if ( counterHint3 >= 25 ) {
			setContent("I'm sure this one will take you through a good path ;)");
		}
		counterHint3--;
	}

	void hint4() {
		if ( counterHint4 >= 25 ) {
			setContent("I'm sensing something good around here pal!");
		}
		counterHint4--;
	}

	void hint5() {
		if ( counterHint5 >= 25 ) {
			setContent("Have you collected the 2 passwords? Use this platform to reach the roof then!");
		}
		counterHint5--;
	}

	void hint6() {
		if ( counterHint6 >= 25 ) {
			setContent("I see 2 platforms you can use to get to the roof. Play smart ;)");
		}
		counterHint6--;
	}

	void hint7() {
		if ( counterHint7 >= 25 ) {
			setContent("Jump to the vertical platform and leave this place now!!!");
		}
		counterHint7--;
	}

	void hint8() {
		if ( counterHint8 >= 25 ) {
			setContent("You're almost done! Jump to the horizontal platform to reach the elevator!!");
		}
		counterHint8--;
	}

	void password() {
		if ( counterPassword >= 10 ) {
			setContent("Hurry kiddo, you need the password notes to open this door!!!");
		}
		counterPassword--;
	}

	void roofL() {
		if ( counterRoofL >= 10 ) {
			setContent("You have rescued me pal. Now it's my turn to battle. Take your glider and fly!!!");
		}
		counterRoofL--;
	}

	void roofR() {
		if ( counterRoofR >= 10 ) {
			setContent("There's nothing over here, I think you left your glider to the other side!!");
		}
		counterRoofR--;
	}

	void computer1() {
		if ( counterComputer1 >= 5 ) {
			setContent("Hurry Foxy Ninja, use the ladder and go to the upper floor!!");
		}
		counterComputer1--;
	}

	void computer2() {
		if ( counterComputer2 >= 5 ) {
			setContent("Good!! Break that computer down with your attack H and collect the yellow key!!!!");
		}
		counterComputer2--;
	}

	void saveMe()
    {
        if (counterSaveMe >= 10)
        {
            setContent("Hey Zozzy! Tim Cruiz locked me in a cage, find the first Unlock Button. Save me!");
        }
        counterSaveMe--;
    }

    void doorExplosion()
    {
        if (counterDoorExplosion >= 50)
        {
            setContent("Press H to fire a rocket to that tiny door! I love explosions!");
        }
        counterDoorExplosion--;
    }

    void timNarcisist()
    {
        if (counterTimNarcisist >= 10)
        {
            setContent("You know... Tim Cruiz is a narcisist. Anyway, Press J to hit enemies!");
        }
        counterTimNarcisist--;
    }

    void wtf()
    {
        if (counterWtf >= 100)
        {
            setContent("WTF is that!? D:  Kill him or run!");
        }
        counterWtf--;
    }

    void redKey()
    {
        if (counterRedKey >= 50)
        {
            setContent("You need the RED KEY. Find it and use it here pressing W");
        }
        counterRedKey--;
    }

    void blueKey()
    {
        if (counterBlueKey >= 100)
        {
            setContent("You need the BLUE KEY. Find it and use it here pressing W");
        }
        counterBlueKey--;
    }


    void holyLight()
    {
        if (counterHolyLight >= 5)
        {
            setContent("CAREFUL HERE! This Holy Light converts you to Sciemfology and kills you in 3 secs!");
        }
        counterHolyLight--;
    }


    void unlocking1()
    {
        if (counterUnlocking1 >= 10)
        {
            setContent("Here you are, my guardian! I'm always hungry. FoxyNinja will find the 2nd unlock button.");
        }
        counterUnlocking1--;
    }


    void explainingDetonation()
    {
        if (counterExplainingDetonation >= 10)
        {
            setContent("After you're out of the building, I will mental-detonate the TNT!");
        }
        counterExplainingDetonation--;
    }

    void coolGuys()
    {
        if (counterCoolGuys >= 5)
        {
            setContent("GO GO GO!!! Cool guys don't look at explosions!");
        }
        counterCoolGuys--;
    }

    void tnt()
    {
        if (counterTnt >= 50)
        {
            setContent("This seems to me a good place to put the TNT!  Press W");
        }
        counterTnt--;
    }

    void karmaCoin()
    {
        if (counterKarmaCoin >= 100)
        {
            setContent("This is a Karma Coin: it gives you some health and points");
        }
        counterKarmaCoin--;
    }

    void correctKey()
    {
        if (counterCorrectKey >= 2)
        {
            setContent("Scroll the inventary (K) and select the correct key! Use it pressing W");
        }
        counterCorrectKey--;
    }

    void explainButtonsOpenUse()
    {
        if (counterExplainButtonsOpenUse >= 25)
        {
            setContent("Press W to enter doors, activate buttons, use inventory objects.");
        }
        counterExplainButtonsOpenUse--;
    }

    void tntPlaced()
    {
        if (counterTntPlaced >= 5)
        {
            setContent("Now GO OUT of the building. I will mental-detonate the TNT.");
        }
        counterTntPlaced--;
    }

    void explainAllButtons()
    {
        if (counterExplainAllButtons >= 5)
        {
            setContent("Move: WASD. \n Attack: H/J. \n Jump: Space. \n PressButtons: W");
        }
        counterExplainAllButtons--;
    }

    void playerDead()
    {
        if (counterPlayerDead >= 1)
        {
            setContent("YOU DIED, SHAME ON YOU! -20 SCORE");
        }
        counterPlayerDead--; 
    }



    private void setContent(string message) {
		messageText.text = message;
		anim.SetTrigger ("play");
	}

	private static void resetCounters() {
		counterAcid = counterAcid == 0 ? 15 : counterAcid;
		counterLadder = counterLadder == 0 ? 50 : counterLadder;
		counterIce = counterIce == 0 ? 25 : counterIce;
		counterMagma = counterMagma == 0 ? 25 : counterMagma;
		counterGameSewer = counterGameSewer == 0 ? 15 : counterGameSewer;
		counterLift = counterLift == 0 ? 10 :  counterLift;
		counterFlame = counterFlame == 0 ? 10 : counterFlame;
		counterMiB = counterMiB == 0 ? 10 : counterMiB;
		counterPlatform = counterPlatform == 0 ? 5 : counterPlatform;
		counterCoinRoom = counterCoinRoom == 0 ? 5 : counterCoinRoom;
		counterMiB2 = counterMiB2 == 0 ? 5 : counterMiB2;
		counterKey = counterKey == 0 ? 5 : counterKey;
		counterLaser = counterLaser == 0 ? 5 : counterLaser;
		counterPress = counterPress == 0 ? 5 :  counterPress;
		counterHoly = counterHoly == 0 ? 5 :  counterHoly;
		counterYellowKey = counterYellowKey == 0 ? 5 :  counterYellowKey;
		counterLaberint = counterLaberint == 0 ? 10 :  counterLaberint;
		counterPassword = counterPassword == 0 ? 5 : counterPassword ;
		counterGreenKey = counterGreenKey == 0 ? 5 : counterGreenKey;
		counterRoofL = counterRoofL == 0 ? 10 : counterRoofL;
		counterRoofR = counterRoofR == 0 ? 10 : counterRoofR;
		counterLaberintL = counterLaberintL == 0 ? 5 : counterLaberint;
		counterHint1 = counterHint1 == 0 ? 25 : counterHint1;
		counterHint2 = counterHint2 == 0 ? 25 : counterHint2;
		counterHint2b = counterHint2b == 0 ? 25 : counterHint2b;
		counterHint3 = counterHint3 == 0 ? 25 : counterHint3;
		counterHint4 = counterHint4 == 0 ? 25 : counterHint4;
		counterHint5 = counterHint5 == 0 ? 25 : counterHint5;
		counterHint6 = counterHint6 == 0 ? 25 : counterHint6;
		counterHint7 = counterHint7 == 0 ? 25 : counterHint7;
		counterHint8 = counterHint8 == 0 ? 25 : counterHint8;
		//counterHint1 = counterHint1 == 0 ? 5 : counterHint1;

        //ZozzyFirstLevel:
        counterSaveMe = counterSaveMe == 0 ? 10 : counterSaveMe;
        //counterDoorExplosion = counterDoorExplosion == 0 ? 50 : counterDoorExplosion;
        counterTimNarcisist = counterTimNarcisist == 0 ? 10 : counterTimNarcisist;
        //counterWtf = counterWtf == 0 ? 100 : counterWtf;
        counterRedKey = counterRedKey == 0 ? 50 : counterRedKey;
        counterBlueKey = counterBlueKey == 0 ? 100 : counterBlueKey;
        counterHolyLight = counterHolyLight == 0 ? 5 : counterHolyLight;
        counterUnlocking1 = counterUnlocking1 == 0 ? 10 : counterUnlocking1;
        counterExplainingDetonation = counterExplainingDetonation == 0 ? 10 : counterExplainingDetonation;
        counterCoolGuys = counterCoolGuys == 0 ? 5 : counterCoolGuys;
        counterTnt = counterTnt == 0 ? 50 : counterTnt;
        //counterKarmaCoin = counterKarmaCoin == 0 ? 100 : counterKarmaCoin;
        counterCorrectKey = counterCorrectKey == 0 ? 2 : counterCorrectKey;
        counterExplainButtonsOpenUse = counterExplainButtonsOpenUse == 0 ? 25 : counterExplainButtonsOpenUse;
        //counterTntPlaced = counterTntPlaced == 0 ? 5 : counterTntPlaced;
        counterExplainAllButtons = counterExplainAllButtons == 0 ? 5 : counterExplainAllButtons;
        counterPlayerDead = counterPlayerDead == 0 ? 1 : counterPlayerDead;
    }

}
