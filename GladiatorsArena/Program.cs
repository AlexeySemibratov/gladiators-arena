
using GladiatorsArena.Arena;
using GladiatorsArena.Heroes;
using GladiatorsArena.Heroes.AncientGolem;
using GladiatorsArena.Heroes.ChaosKnight;
using GladiatorsArena.Heroes.Vampire;

var factory = new HeroFactory();
var hero1 = factory.CreateVampire("Вампир");
var hero2 = factory.CreateAncientGolem("Голем");
var hero3 = factory.CreateWarrior("Воин");
var hero4 = factory.CreateMage("Маг");
var hero5 = factory.CreateChaosKnight("Рыцарь Хаоса");

var arena = new Arena(hero1, hero2);
arena.StartBattle();


