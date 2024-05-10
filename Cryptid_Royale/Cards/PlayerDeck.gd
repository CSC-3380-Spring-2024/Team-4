extends Node

<<<<<<< Updated upstream
<<<<<<< Updated upstream
static var CardList = ["Barghest","GoatMan","Megalodon","Mapinguari","LeechMan"]
=======
static var CardList = ["Barghest","Bigfoot","Megalodon","Mapinguari","Chupacabra"]
>>>>>>> Stashed changes
=======
static var CardList = ["Barghest","Bigfoot","Megalodon","Mapinguari","Chupacabra"]
>>>>>>> Stashed changes

func change(rep: String, with: String):
    var index = CardList.search(rep)
    CardList[index] = with
