function solve() {
    const addButton=document.getElementById('add-btn')
    const infoFragment=document.createDocumentFragment();
    let nameInputElement=document.getElementById('name');
    let phoneInputElement=document.getElementById('phone');
    let categorynputElement=document.getElementById('category');
    const ulElement=document.getElementById('check-list');
   
   
    addButton.addEventListener('click', ()=>{
     
     if(nameInputElement.value!=''&&phoneInputElement.value!=''&&categorynputElement.value!=''){
     const liElement=document.createElement('li')
     const articleElement=document.createElement('article');
     const nameElement=document.createElement('p');
     const phoneElement=document.createElement('p');
     const categoryElement=document.createElement('p');
     const divElement=document.createElement('div');
     const editbuttonElement=document.createElement('button');
     const savebuttonElement=document.createElement('button');
     const name=nameInputElement.value;
     const phone=phoneInputElement.value;
     const category=categorynputElement.value;
     infoFragment.appendChild(liElement);
     liElement.appendChild(articleElement);
     liElement.appendChild(divElement);
     articleElement.appendChild(nameElement);
     articleElement.appendChild(phoneElement);
     articleElement.appendChild(categoryElement);
     divElement.appendChild(editbuttonElement);
     divElement.appendChild(savebuttonElement);
     divElement.classList.add('buttons');
     editbuttonElement.classList.add('edit-btn');
     savebuttonElement.classList.add('save-btn');
     nameElement.textContent='name:'+nameInputElement.value;
     phoneElement.textContent='phone:'+phoneInputElement.value;
     categoryElement.textContent='category:'+categorynputElement.value;
     nameInputElement.value='';
     phoneInputElement.value='';
     categorynputElement.value='';
     ulElement.appendChild(infoFragment);
     editbuttonElement.addEventListener('click',()=>{
         nameInputElement.value=name;
         phoneInputElement.value=phone;
         categorynputElement.value=category;
         ulElement.removeChild(liElement);
             
   
     })
     savebuttonElement.addEventListener('click',()=>{
       
       const contanctUlElement=document.getElementById('contact-list')
       liElement.removeChild(divElement)
    
       const deleteButtonElement=document.createElement('button');
       deleteButtonElement.classList.add('del-btn')
       liElement.appendChild(deleteButtonElement);
       contanctUlElement.appendChild(liElement);
       
       deleteButtonElement.addEventListener('click',()=>{
         deleteButtonElement.parentElement.remove();
         
       })
     })
   
    }})
   }