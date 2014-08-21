using UnityEngine;
using System.Collections;


// Contains all of the math tools I need to use in other scripts

public class MathTools : MonoBehaviour {

	// Generates a list of primes from 2-20
	public static int[] GeneratePrimes(){
		int[] primeList = new int[8];
		primeList[0] = 2;
		primeList[1] = 3;
		primeList[2] = 5;
		primeList[3] = 7;
		primeList[4] = 11;
		primeList[5] = 13;
		primeList[6] = 17;
		primeList[7] = 19;
		return primeList;
	}

	// Creates a list of prime factors of an inputted number
	public static ArrayList GetFactors(int num){
		int i = 0;
		int newNumber = num;
		int[] primeList = new int[8];
		ArrayList factorList = new ArrayList();

		factorList.Clear ();
		primeList = GeneratePrimes ();
		while (i < 8 && primeList[i] < num){
			if (newNumber % primeList[i] == 0){
				factorList.Add (primeList[i]);
				newNumber = newNumber / primeList[i];
			}
			else
				i++;
		}
		return factorList;
	}

}
