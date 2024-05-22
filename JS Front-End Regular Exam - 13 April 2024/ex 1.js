function solve(input){
    const n = Number(input.shift())
    let all = {}
    for (let index = 0; index < n; index++) {
        const[name,HP,bullets]=input[index].split(' ');
        all[name]={
            HP,
           bullets
        }
        
    }
    let command=input.shift();
    while(command!='Ride Off Into Sunset'){
        const [type,name,purvo, vtoro]=command.split(' - ')
        let character=all[name];
        if (type=='FireShot') {
            if (character.bullets>0) {
                character.bullets-=1;
                console.log(`${name} has successfully hit ${purvo} and now has ${character.bullets} bullets!`)
            }
            else {
                console.log(`${name} doesn't have enough bullets to shoot at ${purvo}!`)
            }
        }
        else if(type=='TakeHit'){
            character.HP-=purvo;
            if(character.HP>0){
                console.log(`${name} took a hit for ${purvo} HP from ${vtoro} and now has ${character.HP} HP!`)
            }
            else{
                console.log(`${name} was gunned down by ${vtoro}!`)
            }
        }
        else if (type=='Reload') {
            let bulletsReloaded=6-character.bullets;
            if(character.bullets<6){
                character.bullets=6;
                console.log(`${name} reloaded ${bulletsReloaded} bullets!"`)
            }
            else{
                console.log(`${name}'s pistol is fully loaded!`)
            }
        }
        else if (type=='PatchUp') {

            if(character.HP==100){
                console.log(`${name} is in full health!`)
            }
            else{
                character.HP+=Number(purvo);
                if(character.HP>100){
                    character.HP=100;
                    console.log(`${name} patched up and recovered ${purvo} HP!`)
                }
                else{
                    console.log(`${name} patched up and recovered ${purvo} HP!`)
                }
            }
        }
        command=input.shift();
    }
    for (const Name in all) {
        if(all[Name].HP>0){
        console.log(`${Name}\n HP: ${all[Name].HP}\n Bullets: ${all[Name].bullets}`);}
    }

}