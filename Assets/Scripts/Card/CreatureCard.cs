using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCard: Card 
{
   private int health;
   private int attack;
   private int armor;

   public CreatureCard(Sprite cardFace, Sprite cardBack, int health, int attack, int armor): base(cardFace, cardBack){
       this.health = health;
       this.attack = attack;
       this.armor = armor;
   }

   public int Health{
       get { return this.health; }
       set { this.health = value; }
   }

   public int Attack{
       get { return this.attack; }
       set { this.attack = value; }
   }

   public int Armor{
       get { return this.armor; }
       set { this.armor = value; }
   }
}
