function solve() {
    const inputTypeElement=document.getElementById('type');
   const inputAgeElement=document.getElementById('age');
   const inputGenderElement=document.getElementById('gender');
   const adoptButtonElement=document.getElementById('adopt-btn');
   const listElement=document.getElementById('adoption-info');
   const adoptedList=document.getElementById('adopted-list')
   adoptButtonElement.setAttribute('type','button');
 
   adoptButtonElement.addEventListener('click',()=>{
     
     // if(inputAgeElement.value=='' ||inputGenderElement.value==''||inputTypeElement.value==''){
     //   return;
     // }
     let age =inputAgeElement.value;
     let gender=inputGenderElement.value;
     let type=inputTypeElement.value;
     const liEl=document.createElement('li');
     const articleElement=document.createElement('article');
     const typeElement=document.createElement('p');
     const genderElement=document.createElement('p');
     const ageElement=document.createElement('p');
     const divbtnElement=document.createElement('div');
     const editbtnElement=document.createElement('button');
     const donebtnElement=document.createElement('button');
     divbtnElement.classList.add('buttons');
     editbtnElement.classList.add('edit-btn');
     donebtnElement.classList.add('done-btn');
     typeElement.textContent=`Pet:${inputTypeElement.value}`;
     genderElement.textContent=`Gender:${inputGenderElement.value}`;
     ageElement.textContent=`Age:${inputAgeElement.value}`;
     editbtnElement.textContent='Edit'
     donebtnElement.textContent='Done'
     listElement.appendChild(liEl);
    
     liEl.appendChild(articleElement);
     liEl.appendChild(divbtnElement);
     articleElement.appendChild(typeElement);
     articleElement.appendChild(genderElement);
     articleElement.appendChild(ageElement);
     divbtnElement.appendChild(editbtnElement);
     divbtnElement.appendChild(donebtnElement);
     
     inputAgeElement.value='';
     inputGenderElement.value='';
      inputTypeElement.value='';
 
     editbtnElement.addEventListener('click',()=>{
        inputAgeElement.value=age;
        inputGenderElement.value=gender;
        inputTypeElement.value=type;
        listElement.removeChild(liEl);
         
     })
     donebtnElement.addEventListener('click',()=>{
       
       
       liEl.removeChild(divbtnElement);
       const clearbtn=document.createElement('button');
       clearbtn.classList.add('clear-btn')
       clearbtn.textContent='Clear'
       liEl.appendChild(clearbtn);
       adoptedList.appendChild(liEl)
       
       clearbtn.addEventListener('click',()=>{
         liEl.remove();
         
       
       })
     })
 
   })
   }
   