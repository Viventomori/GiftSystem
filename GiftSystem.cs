using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Assertions;
using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.Popups;
using GameVanilla.Game.UI;



namespace GameVanilla.Game.Scenes
{

    [Serializable]
    public class GiftSystem : BaseScene
    {
        private Action<int> onFoodUpdate;

        [Header("Delay for Popup")]
        [SerializeField]
        private int delayPopup;

        [Header("How much food need to Gift")]

        [SerializeField]
        private int firstGift;

        [SerializeField]
        private int secondGift;

        [SerializeField]
        private int threeGift;

        [SerializeField]
        private int fourGift;

        [SerializeField]
        private int fiveGift;

        [Header("Coins for pet")]

        [SerializeField]
        private List<int> listCoin = new List<int>();

        [Header("Booster Loli")]
        [SerializeField]
        private List<int> listBooster0 = new List<int>();

        [Header("Booster Bomb")]
        [SerializeField]
        private List<int> listBooster1 = new List<int>() { };

        [Header("Booster Switch")]
        [SerializeField]
        private List<int> listBooster2 = new List<int>() { };

        [Header("Booster Color Bomb")]
        [SerializeField]
        private List<int> listBooster3 = new List<int>() { };

        [Header("Counts")]
        [SerializeField]
        private int foodCount;

        [SerializeField]
        private int countListGifts;

        public int countBarGift = 0;

        [SerializeField]
        private Text countFoodTop;

        [SerializeField]
        private Text countFoodBot;

        [SerializeField]
        private Text countLVLTop;

        [Header("Data for actuality")]
        public int currentLvl = 1;
        public int actullyFood;
        public int maxProgressLvl;
        public int addCountProgress;

        private int coinGift;

        [Header("Other scripts")]
        public FoodBar foodBar;
        public PetAnim petAnim;



        private void Start()
        {
            foodCount = PlayerPrefs.GetInt("num_foods");
            actullyFood = PlayerPrefs.GetInt("have_food");
            currentLvl = PlayerPrefs.GetInt("lvl_progress");
            foodBar.SetMaxProgress(maxProgressLvl);
            foodBar.SetProgress(actullyFood);


            countBarGift = PlayerPrefs.GetInt("countGift");
            coinGift = PlayerPrefs.GetInt("num_coins");

        }


        private void Update()
        {
            countBarGift = PlayerPrefs.GetInt("countGift");

            if (actullyFood >= firstGift && countBarGift == 0)
            {
                AddGifts();
                StartCoroutine(PrizePopup0());
            }

            if (actullyFood >= secondGift && countBarGift == 2)
            {
                AddGifts();
                StartCoroutine(PrizePopup1());
            }

            if (actullyFood >= threeGift && countBarGift == 4)
            {
                AddGifts();
                StartCoroutine(PrizePopup2());
            }

            if (actullyFood >= fourGift && countBarGift == 6)
            {
                AddGifts();
                StartCoroutine(PrizePopup3());
            }

            if (actullyFood >= fiveGift && countBarGift == 8)
            {
                AddGifts();
                StartCoroutine(PrizePopup4());
                NextLvlPet();
            }

            countFoodTop.text = "" + actullyFood;
            countFoodBot.text = "" + foodCount;
            countLVLTop.text = "Level " + currentLvl;

        }
        public void TakeFood(int amount)
        {
            var numFood = PlayerPrefs.GetInt("num_foods");
            numFood += amount;
            PlayerPrefs.SetInt("num_foods", numFood);
            if (onFoodUpdate != null)
            {
                onFoodUpdate(numFood);
            }
            foodCount = numFood;
        }

        /// <summary>
        /// Spends the specified amount of food.
        /// </summary>
        /// <param name="amount">The amount of food to spend.</param>
        public void SpendFood(int amount)
        {
            if (foodCount != 0)
            {
                var numFood = PlayerPrefs.GetInt("num_foods");
                var saveFood = PlayerPrefs.GetInt("have_food");
                amount = numFood;
                actullyFood = saveFood;
                saveFood += amount;
                PlayerPrefs.SetInt("have_food", saveFood);
                numFood -= amount;
                if (numFood < 0)
                {
                    numFood = 0;
                }
                PlayerPrefs.SetInt("num_foods", numFood);
                if (onFoodUpdate != null)
                {
                    onFoodUpdate(numFood);
                }
                foodCount = numFood;
                foodCount = PlayerPrefs.GetInt("num_foods");
                actullyFood = PlayerPrefs.GetInt("have_food");
                foodBar.SetProgress(actullyFood);
                petAnim.PetFeed();
                Debug.Log("work");
            }
        }
        /// <summary>
        /// для тестов(сброс)
        /// </summary>
        public void ResetCountFood()
        {
            actullyFood = 0;
            currentLvl = 1;
            countBarGift = 0;
            countListGifts = 0;
            PlayerPrefs.SetInt("have_food", actullyFood);
            actullyFood = PlayerPrefs.GetInt("have_food");
            PlayerPrefs.SetInt("lvl_progress", currentLvl);
            currentLvl = PlayerPrefs.GetInt("lvl_progress");
            PlayerPrefs.SetInt("countGift", countBarGift);
            countBarGift = PlayerPrefs.GetInt("countGift");
            PlayerPrefs.SetInt("countListGifts", countListGifts);
            countListGifts = PlayerPrefs.GetInt("countListGifts");
            foodBar.SetProgress(actullyFood);
        }


        /// booster 0 - loli
        /// booster 1 - bomb
        /// booster 2 - switch
        /// booster 3 - colorbomb
        public void AddGifts()
        {

            countListGifts = PlayerPrefs.GetInt("countListGifts");


            PlayerPrefs.SetInt("countGift", countBarGift);
            var coins = listCoin[countListGifts];
            coinGift += coins;
            PlayerPrefs.SetInt("num_coins", coinGift);

            var numBooster0 = PlayerPrefs.GetInt("num_boosters_0");
            var booster0 = listBooster0[countListGifts];
            numBooster0 += booster0;
            PlayerPrefs.SetInt("num_boosters_0", numBooster0);
            numBooster0 = PlayerPrefs.GetInt("num_boosters_0");

            var numBooster1 = PlayerPrefs.GetInt("num_boosters_1");
            var booster1 = listBooster1[countListGifts];
            numBooster1 += booster1;
            PlayerPrefs.SetInt("num_boosters_1", numBooster1);
            numBooster1 = PlayerPrefs.GetInt("num_boosters_1");

            var numBooster2 = PlayerPrefs.GetInt("num_boosters_2");
            var booster2 = listBooster2[countListGifts];
            numBooster2 += booster2;
            PlayerPrefs.SetInt("num_boosters_2", numBooster2);
            numBooster2 = PlayerPrefs.GetInt("num_boosters_2");

            var numBooster3 = PlayerPrefs.GetInt("num_boosters_3");
            var booster3 = listBooster3[countListGifts];
            numBooster3 += booster3;
            PlayerPrefs.SetInt("num_boosters_3", numBooster3);
            numBooster3 = PlayerPrefs.GetInt("num_boosters_3");


            countBarGift = PlayerPrefs.GetInt("countGift");
            countBarGift++;
            PlayerPrefs.SetInt("countGift", countBarGift);

            countListGifts++;

            PlayerPrefs.SetInt("countListGifts", countListGifts);
            Debug.Log(numBooster0);
            Debug.Log(numBooster1);
            Debug.Log(numBooster2);
            Debug.Log(numBooster3);
        }

        public void AddCount()
        {
            countBarGift = PlayerPrefs.GetInt("countGift");
            countBarGift++;
            PlayerPrefs.SetInt("countGift", countBarGift);
        }

        public void NextLvlPet()
        {
            currentLvl++;
            PlayerPrefs.SetInt("lvl_progress", currentLvl);
            actullyFood -= 30;
            PlayerPrefs.SetInt("have_food", actullyFood);
            actullyFood = PlayerPrefs.GetInt("have_food");
            maxProgressLvl += addCountProgress;
            countBarGift = -1;
            PlayerPrefs.SetInt("countGift", countBarGift);

            foodBar.SetProgress(actullyFood);
        }


        /// booster 0 - loli
        /// booster 1 - bomb
        /// booster 2 - switch
        /// booster 3 - colorbomb
        IEnumerator PrizePopup0()
        {
            yield return new WaitForSeconds(delayPopup);
            OpenPopup<Prize0Pet>("Popups/Pet/Prize0Pet", popup =>
            {
                popup.SetTitle("You Win!");
                popup.SetTextCoins("x" + listCoin[0]);
            }, false);
        }

        IEnumerator PrizePopup1()
        {
            yield return new WaitForSeconds(delayPopup);
            OpenPopup<Prize1Pet>("Popups/Pet/Prize1Pet", popup =>
            {
                popup.SetTitle("You Win!");
                popup.SetTextBooster0("x1");
            }, false);
        }

        IEnumerator PrizePopup2()
        {
            yield return new WaitForSeconds(delayPopup);
            OpenPopup<Prize2Pet>("Popups/Pet/Prize2Pet", popup =>
            {
                popup.SetTitle("You Win!");
                popup.SetTextBooster0("x1");
                popup.SetTextBooster2("x1");

            }, false);
        }

        IEnumerator PrizePopup3()
        {
            yield return new WaitForSeconds(delayPopup);
            OpenPopup<Prize3Pet>("Popups/Pet/Prize3Pet", popup =>
            {
                popup.SetTitle("You Win!");
                popup.SetTextCoins("x" + listCoin[5]);
                popup.SetTextBooster1("x2");
            }, false);
        }

        IEnumerator PrizePopup4()
        {
            yield return new WaitForSeconds(delayPopup);
            OpenPopup<Prize4Pet>("Popups/Pet/Prize4Pet", popup =>
            {
                popup.SetTitle("You Win!");
                popup.SetTextCoins("x" + listCoin[10]);
                popup.SetTextBooster1("x2");
                popup.SetTextBooster2("x2");
                popup.SetTextBooster3("x2");
            }, false);
        }

        //testBoolean
        public void ResetBoosterCoin()
        {
            coinGift = 0;
            PlayerPrefs.SetInt("num_coins", coinGift);
            coinGift = PlayerPrefs.GetInt("num_coins");
            var numBooster0 = 0;
            PlayerPrefs.SetInt("num_boosters_0", numBooster0);
            numBooster0 = PlayerPrefs.GetInt("num_boosters_0");
            var numBooster1 = 0;
            PlayerPrefs.SetInt("num_boosters_1", numBooster1);
            numBooster1 = PlayerPrefs.GetInt("num_boosters_1");
            var numBooster2 = 0;
            PlayerPrefs.SetInt("num_boosters_2", numBooster2);
            numBooster2 = PlayerPrefs.GetInt("num_boosters_2");
            var numBooster3 = 0;
            PlayerPrefs.SetInt("num_boosters_3", numBooster3);
            numBooster3 = PlayerPrefs.GetInt("num_boosters_3");

            Debug.Log(coinGift);
            Debug.Log(numBooster0);
            Debug.Log(numBooster1);
            Debug.Log(numBooster2);
            Debug.Log(numBooster3);
        }
    }
}
