const baseUrl = 'http://localhost:3030/jsonstore/tasks';

const loadButtonElement= document.getElementById('load-meals');
const mealListElement=document.getElementById('list');
const loadMeals = async () => {
    //fetch all meals
    const response = await fetch(baseUrl);
    const data = await response.json();
    mealListElement.innerHTML='';
    for (const meal of Object.values(data)) {
        const deleteButtonElement=document.createElement('button');
        deleteButtonElement.classList.add('delete-meal');
        const changeButtonElement=document.createElement('button');
        changeButtonElement.classList.add('change-meal');
        const divButtonsElement=document.createElement('div');
        divButtonsElement.id='meal-buttons';
        divButtonsElement.appendChild(changeButtonElement)
        divButtonsElement.appendChild(deleteButtonElement);
        const mealNameElement=document.createElement('h2');
        mealNameElement.textContent=meal.food;
        const timeElement=document.createElement('h3')
        timeElement.textContent=meal.time;
        const caloriesElement=document.createElement('h3')
        caloriesElement.textContent=meal.calories;
        const mealElement=document.createElement('div');
        mealElement.id='meal';
        mealElement.appendChild(mealNameElement);
        mealElement.appendChild(timeElement);
        mealElement.appendChild(caloriesElement);
        mealElement.appendChild(divButtonsElement);
        
        mealListElement.appendChild(mealElement);
    }


};
loadButtonElement.addEventListener('click', loadMeals);