const baseUrl='http://localhost:3030/jsonstore/games'; 
const loadGamesElement=document.getElementById('load-games');
const gamesListElement=document.getElementById('games-list');
const addGameButton=document.getElementById('add-game');
const nameInputElement=document.getElementById('g-name');
const typeInputElement=document.getElementById('type');
const maxplayersInputElement=document.getElementById('players');
const editButtonElement=document.getElementById('edit-game');
const formElemet=document.getElementById('form-section');

loadGamesElement.addEventListener('click',loadGames);
editButtonElement.addEventListener('click',()=>{
    const game=getInput();
    const gameid=formElemet.getAttribute('data-id');
    formElemet.removeAttribute('data-id');

    fetch(`${baseUrl}/${gameid}`,{
        method:'PUT',
        headers:{
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({_id:gameid,name:game.name,type:game.type,players:game.players})

    }).then(res=>{
        if(!res.ok){
            return;
        }
        loadGames();
        addGameButton.removeAttribute('disabled');
    editButtonElement.setAttribute('disabled','disabled');
    nameInputElement.value='';
    typeInputElement.value='';
    maxplayersInputElement.value='';
    })
})
async function loadGames(){
    const response = await fetch(baseUrl);
    const data= await response.json();
    gamesListElement.innerHTML='';
    for (const game of Object.values(data)) {
        gamesListElement.appendChild(createGame(game.name,game.type,game.players,game._id));
    }
}

addGameButton.addEventListener('click',()=>{
    
    const newGame=getInput();
     fetch(baseUrl,{
        method:'POST',
        headers:{
            'content-type':'application/json'
        },
        body: JSON.stringify(newGame)
     })
     .then(res=>{
        if(!res.ok){
            return;
        }
        nameInputElement.value='';
        typeInputElement.value='';
        maxplayersInputElement.value='';
        loadGames()
     })
})


function getInput(){
    return{
        name:nameInputElement.value,
        type:typeInputElement.value,
        players:maxplayersInputElement.value
    }
}


function createGame(name, type, maxplayers, id){
 const bigDivElement=document.createElement('div');
const contentDivElement=document.createElement('div');
const gameNameElement=document.createElement('p');
const maxPlayersElement=document.createElement('p');
const gameTypeElement=document.createElement('p');
const buttonsDivElement=document.createElement('div');
const changeButtonElement=document.createElement('button');
const deleteButtonElement=document.createElement('button');
bigDivElement.classList.add('board-game');
contentDivElement.classList.add('content');
buttonsDivElement.classList.add('buttons-container');
changeButtonElement.classList.add('change-btn');
deleteButtonElement.classList.add('delete-btn');
gameNameElement.textContent=name;
gameTypeElement.textContent=type;
maxPlayersElement.textContent=maxplayers;
changeButtonElement.textContent='Change';
deleteButtonElement.textContent='Delete';
bigDivElement.setAttribute('data-id',id);
bigDivElement.appendChild(contentDivElement);
bigDivElement.appendChild(buttonsDivElement);
contentDivElement.appendChild(gameNameElement);
contentDivElement.appendChild(gameTypeElement);
contentDivElement.appendChild(maxPlayersElement);

buttonsDivElement.appendChild(changeButtonElement);
buttonsDivElement.appendChild(deleteButtonElement);

deleteButtonElement.addEventListener('click',()=>{
    fetch(`${baseUrl}/${id}`,{
        method:'DELETE',
    }).then(res=>{
        if(!res.ok){
            return;
        }
        loadGames()
    })

})
changeButtonElement.addEventListener('click', ()=>{
    
    nameInputElement.value=name;
    typeInputElement.value=type;
    maxplayersInputElement.value=maxplayers;
    formElemet.setAttribute('data-id',id);
    editButtonElement.removeAttribute('disabled');
    addGameButton.setAttribute('disabled','disabled')
    
    bigDivElement.remove();
})
return bigDivElement;
}