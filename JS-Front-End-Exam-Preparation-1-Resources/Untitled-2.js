function solve(input) {
    const baristaCount=Number(input.shift())
    let team = {}
    for (let index = 0; index < baristaCount; index++) {
        const[name,shift,drinks]=input[index].split(' ');
        team[name]={
            shift,
            drinks:drinks.split(',')
        }
        
    }
    let commandLine=input.shift()
    while(commandLine!='Closed'){
        const [command,name,first,second]=commandLine.split(' / ');
        let barista=team[name];
        if (command=='Prepare') {
            if (barista.shift==first&&barista.drinks.includes(second)) {
                console.log(`${name} has prepared a ${second} for you!`)
            }
            else{
                console.log(`${name} is not available to prepare a ${second}.`)
            }
            
        }
        else if(command=='Change Shift') {
            team[name].shift=first;
            console.log(`${name} has updated his shift to: ${first}`);
        }
        else if(command=='Learn'){
            if(barista.drinks.includes(first)){
                console.log(`${name} knows how to make ${first}.`);
            }
            else{
                team[name].drinks.push(first);
                console.log(`${name} has learned a new coffee type: ${first}.`);

            }
        }
        

        commandLine=input.shift()
    }   
    for (const barista in team) {
        console.log(`Barista: ${barista}, Shift: ${team[barista].shift}, Drinks: ${team[barista].drinks.join(', ')}`);
    }        
}
solve(['4',

'Alice day Espresso,Cappuccino',

'Bob night Latte,Mocha',

'Carol day Americano,Mocha',

'David night Espresso',

'Prepare / Alice / day / Espresso',

'Change Shift / Bob / day',

'Learn / Carol / Latte',

'Prepare / Bob / night / Latte',

'Learn / David / Cappuccino',
'Prepare / Carol / day / Cappuccino', 'Change Shift / Alice / night', 'Learn / Bob / Mocha', 'Prepare / David / night / Espresso', 'Closed']
);