let characterSelector = true;

function ChangingValueTo(id, value) {
    document.getElementById(id).innerHTML = value;
}

function GetValueOf(id) {
        return document.getElementById(id).value;
}

class Character{
    name;
    race;
    chClass;
    level;
    hitdie;
    stats = [0,0,0,0,0,0];
    maxhp;
    currenthp;
    constructor(name, race,chClass,level,hitdie,stats,maxhp,currenthp) {
        this.name = name;
        this.race = race;
        this.chClass = chClass;
        this.level = level;
        this.hitdie = hitdie;
        this.stats = stats;
        this.maxhp = maxhp;
        this.currenthp = currenthp;
    }

    getLvl(){
        return this.level;
    }
    getHitDie(){
        return this.hitdie;
    }
    getName() {
        return this.name;
    }
    getClass() {
        return this.chClass;
    }
    getRace() {
        return this.race;
    }
    getMaxHp(){
        return this.maxhp;
    }
    getCurrentHp(){
        return this.currenthp;
    }
    changeCurrentHpBy(value){
        this.currenthp += value;
    }
    GetStat(which){
        return this.stats[which];
    }
    getDiscription() {
        return this.getName() + ", " + this.getRace() + ", " + this.getClass() + ", " + this.getLvl() +"-th lvl, " +this.getCurrentHp() + "/" + this.getMaxHp() +
        " hp, STR: " +  this.GetStat(0) + " DEX: "+  this.GetStat(1) + " CON: "+  this.GetStat(2) + " INT: "+  this.GetStat(3) + " WIS: "+  this.GetStat(4) + " CHA: " + this.GetStat(5);
    }
}

class CharacterForSending {
    name;
    race;
    chClass;
    level;
    stats = [0, 0, 0, 0, 0, 0];
    average;
    constructor(name, race, chClass, level, stats,average) {
        this.name = name;
        this.race = race;
        this.chClass = chClass;
        this.level = level;
        this.stats = stats;
        this.average = average;
    }
}

function readInStats(){
    let stats = [];
    stats[0] = GetValueOf("Strenght");
    stats[1] = GetValueOf("Dexterity");
    stats[2] = GetValueOf("Constitution");
    stats[3] = GetValueOf("Intelligence");
    stats[4] = GetValueOf("Wisdom");
    stats[5] = GetValueOf("Charisma");
    return stats;
}

function CreateNewCharacterfromInput() {
    return new CharacterForSending(GetValueOf("Icname"), GetValueOf("races"), GetValueOf("classes"), GetValueOf("level"), readInStats(), GetValueOf("average"));
}

function updateChSelector() {

    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == XMLHttpRequest.DONE) {
            if (xhttp.status == 200) {
                console.info("Response: " + xhttp.responseText);

                var selector = document.getElementById("CharacterSelection");
                while (selector.length > 0) {
                    selector.remove(selector.length - 1);
                }
                var incomingCharacters = JSON.parse(xhttp.responseText);
                for (let index = 0; index < incomingCharacters.length; index++) {
                    var newOption = document.createElement("option");
                    newOption.value = incomingCharacters[index]["index"];
                    newOption.text = incomingCharacters[index]["name"];
                    selector.add(newOption);
                }
                console.info(xhttp.responseText);
            }
            else if (xhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('Server error: something else other than 200 was returned');
            }
        }
    };

    xhttp.open("GET", "http://localhost:5189/refreshCharacters", true);
    xhttp.send();
}

function SwitchSelecToCreateOrViseversa(){
    if (characterSelector) {
        updateChSelector();
        document.getElementById("CharacterCreator").style.display = "none";
        document.getElementById("CharacterSelector").style.display = "block";
        characterSelector = false;
    }
    else{
        document.getElementById("CharacterSelector").style.display = "none";
        document.getElementById("CharacterCreator").style.display = "block";
        characterSelector = true;
    }
}

function storeCharacter() {
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == XMLHttpRequest.DONE) {   
            if (xhttp.status == 200) {
                console.info("Response: " + xhttp.responseText);
                if ("Done" == xhttp.responseText)
                    alert("New Character storing finished!");
                console.info(xhttp.responseText);
            }
            else if (xhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('Server error: something else other than 200 was returned');
            }
        }
    };
    newCharacter = CreateNewCharacterfromInput();
    sendingCharacterJSON = JSON.stringify(newCharacter);

    xhttp.open("GET", "http://localhost:5189/storeCharacter?jsonElement=" + sendingCharacterJSON, true);
    xhttp.send();
}

function getChoosenCharacter() {
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == XMLHttpRequest.DONE) {
            if (xhttp.status == 200) {
                console.info("Response: " + xhttp.responseText);
                document.getElementById("characterDescp").innerHTML = JSON.parse(xhttp.responseText);
                console.info(xhttp.responseText);
            }
            else if (xhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('Server error: something else other than 200 was returned');
            }
        }
    };

    optionSelected = document.getElementById("CharacterSelection");
    chSelected = { name: optionSelected.options[optionSelected.selectedIndex].text, index: optionSelected.options[optionSelected.selectedIndex].value };

    xhttp.open("GET", "http://localhost:5189/getCharacter?jsonCharacterRequest=" + chSelected, true);
    xhttp.send();
}