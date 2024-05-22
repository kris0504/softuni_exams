window.addEventListener("load", solve);
function solve(){
    const placeElement=document.getElementById('place');
    const actionElement=document.getElementById('action');
    const personElement=document.getElementById('person');
    const addButtonElement=document.getElementById('add-btn');
    const tasksListElement=document.getElementById('task-list');
    const doneListElement=document.getElementById('done-list');
    let tasks =[];
    addButtonElement.addEventListener('click', ()=>{
        let task={place:placeElement.value,action:actionElement.value,person:personElement.value}
        tasks.push(task);
        let buttonEdit=document.createElement('button');
        buttonEdit.textContent='Edit';
          let buttonDone=document.createElement('button');
        buttonDone.textContent='Done';
        let place=document.createElement('p')
        place.textContent="Place:"+placeElement.value;
        let action=document.createElement('p')
        action.textContent=`Action:${actionElement.value}`;
        let person=document.createElement('p')
        person.textContent="Person:"+personElement.value;
        tasksListElement.appendChild(place);
        tasksListElement.appendChild(action);
        tasksListElement.appendChild(person);
        tasksListElement.appendChild(buttonEdit);
        tasksListElement.appendChild(buttonDone);
        placeElement.value='';
        actionElement.value='';
        personElement.value='';
        buttonEdit.addEventListener('click',()=>{
            placeElement.value=task.place;
            actionElement.value=task.action;
            personElement.value=task.person;
            tasksListElement.removeChild(place);
            tasksListElement.removeChild(action);
            tasksListElement.removeChild(person);
            tasksListElement.removeChild(buttonDone);
            tasksListElement.removeChild(buttonEdit);


        });
        buttonDone.addEventListener('click',()=>{
            tasksListElement.removeChild(place);
            tasksListElement.removeChild(action);
            tasksListElement.removeChild(person);
            tasksListElement.removeChild(buttonDone);
            tasksListElement.removeChild(buttonEdit);
            doneListElement.appendChild(place);
            doneListElement.appendChild(action);
            doneListElement.appendChild(person);
            let buttonDelete=document.createElement('button');
            buttonDelete.textContent='Delete'
     
            doneListElement.appendChild(buttonDelete);
            buttonDelete.addEventListener('click',()=>{
            doneListElement.removeChild(place);
            doneListElement.removeChild(action);
            doneListElement.removeChild(person);
            doneListElement.removeChild(buttonDelete);

            })

        })

    })


}

