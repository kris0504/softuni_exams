const baseUrl='http://localhost:3030/jsonstore/records/'
const listElement=document.getElementById('list')
const loadButtonElement=document.getElementById('load-records');
const nameinputElement=document.getElementById('p-name')
const stepsinputElement=document.getElementById('steps')
const caloriesinputElement=document.getElementById('calories')
const addButtonElement=document.getElementById('add-record')
const editbtnElement=document.getElementById('edit-record')
const formElement=document.getElementById('form');

// window.addEventListener("load", solve);
// function solve(){
//     loadButtonElement.setAttribute('type','button');
// }

loadButtonElement.addEventListener('click', loadRecords)
async function loadRecords() {
    const result = await fetch(baseUrl);
    const data = await result.json();
    listElement.innerHTML = '';
    for (const record of Object.values(data)) {
        listElement.appendChild(createRecord(record.name, record.steps, record.calories, record._id));
    }
}

editbtnElement.addEventListener('click',()=>{
    const newRecord=getInput();
    const id=formElement.getAttribute('data-id');
    formElement.removeAttribute('data-id');
    fetch(`${baseUrl}${id}`,{
        method: 'PUT',
        headers:{'content-type': 'application/json'},
        body:JSON.stringify({name:newRecord.name,steps:newRecord.steps,calories:newRecord.calories,_id:id})
    }).then(res=>{
        if (!res.ok) {
            return;
        }
        nameinputElement.value='';
        stepsinputElement.value='';
        caloriesinputElement.value='';
        addButtonElement.removeAttribute('disabled');
        editbtnElement.setAttribute('disabled', 'disabled');
        loadRecords();
    })
})

addButtonElement.addEventListener('click',()=>{
    const newRecord=getInput();
    fetch(baseUrl,{
        method:'POST',
        headers:{
            'content-type': 'application/json'
        },
        body:JSON.stringify(newRecord)
    }).then(res=>{
        if (!res.ok) {
            return;
        }
        nameinputElement.value='';
        stepsinputElement.value='';
        caloriesinputElement.value='';
        addButtonElement.removeAttribute('disabled');
        editbtnElement.setAttribute('disabled', 'disabled');
        loadRecords();
    })
})


function createRecord(name, steps,calories, id){
    const liElement=document.createElement('li');
    const divInfoElement=document.createElement('div');
    const nameElement=document.createElement('p');
    const stepsElement=document.createElement('p');
    const caloriesElement=document.createElement('p');
    const divbuttonsElement=document.createElement('div');
    const changeButtonElement=document.createElement('button');
    const deleteButtonElement=document.createElement('button');
    liElement.classList.add('record');
    divInfoElement.classList.add('info');
    divbuttonsElement.classList.add('btn-wrapper');
    changeButtonElement.classList.add('change-btn');
    deleteButtonElement.classList.add('delete-btn');
    nameElement.textContent=name;
    stepsElement.textContent=steps;
    caloriesElement.textContent=calories;
    changeButtonElement.textContent='Change';
    deleteButtonElement.textContent='Delete';
    liElement.setAttribute('data-id',id)
    liElement.appendChild(divInfoElement);
    liElement.appendChild(divbuttonsElement);
    divInfoElement.appendChild(nameElement);
    divInfoElement.appendChild(stepsElement);
    divInfoElement.appendChild(caloriesElement);
    divbuttonsElement.appendChild(changeButtonElement);
    divbuttonsElement.appendChild(deleteButtonElement);
    deleteButtonElement.addEventListener('click',()=>{
        fetch(`${baseUrl}${liElement.getAttribute('data-id')}`,{
            method: 'DELETE'

        }).then(res=>{
            if(!res.ok){
                return;
            }
            loadRecords();
        })
    })
    changeButtonElement.addEventListener('click',()=>{
        nameinputElement.value=name;
        stepsinputElement.value=steps;
        caloriesinputElement.value=calories;
        //liElement.remove();
        formElement.setAttribute('data-id',id)
        editbtnElement.removeAttribute('disabled');
        addButtonElement.setAttribute('disabled', 'disabled');
    })


return liElement;


}
function getInput(){
    return{
        name:nameinputElement.value,
        steps:stepsinputElement.value,
        calories:caloriesinputElement.value
    }
}