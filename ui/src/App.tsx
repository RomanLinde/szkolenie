import React, { useEffect, useState } from 'react';
import './App.css';
import { Button, MenuItem, Select, TextField } from '@material-ui/core';
import ButtonP from './ButtonP';
import styled from 'styled-components';

interface ITowar {
  id: string
  nazwa: string
}

const fetchData = (url:string, data:object) => {
  const headers = new Headers();
  headers.set("Content-Type", "application/json;charset=UTF-8");
  headers.set("Accept", "application/json;charset=UTF-8");
  console.log("body:", data)
  const body = JSON.stringify(data);

  const req = fetch('http://localhost:5000/'+url, {
    method: 'POST',
    headers,
    body
  })
  return req.then((resp) => {
    if (resp.status !== 204) return resp.json();    
  })
}

const DivApp = styled.div`
  width: 800px;
  margin: 0 auto;
`
function App() {
  const [lista, zmienListe] = useState([] as ITowar[])
  const [nazwaTowaru, zmienNazwaTowaru] = useState("")
  const [idTowaru, zmienIdTowaru] = useState("")

  useEffect(() => {
    fetchData('towary/daj', {}).then(json => {
      zmienListe(json)
    })
  }, [])

  const reload = () => {
    zmienListe([])
    fetchData('towary/daj', {}).then((json:ITowar[]) => {
      zmienListe(json)      
    })
  } 

  const zapis = (e:any) => {
    e.preventDefault();
    fetchData('towary/zapisz', {nazwa: nazwaTowaru, id:idTowaru})
      .then(v => {
        zmienNazwaTowaru("")
        zmienIdTowaru("")
        reload()
      })
  }

  const usunTowar = () => {
    fetchData('towary/usun', { id:idTowaru})
      .then(() => {
        zmienNazwaTowaru("")
        zmienListe(prev => prev.filter(x => x.id!==idTowaru))
        zmienIdTowaru("")
      })
  }

  return (
    <DivApp>
      <Select value={idTowaru} onChange={(e:any) => zmienIdTowaru(e.target.value)}>
        {lista.map((x:ITowar) => (<MenuItem key={x.id} value={x.id}>{x.nazwa}</MenuItem>))}
      </Select>
      <Button onClick={usunTowar}>Usuń wybrany</Button>
      <form onSubmit={(e:any)=>zapis(e)}>
        <TextField type="text" name="nazwa" value={nazwaTowaru} onChange={(e:any)=>zmienNazwaTowaru(e.target.value)}/>
        <input type="submit" value="Zapisz"/>
      </form>
      <ButtonP onClick={reload}>
        <div>Dodaj</div>
      </ButtonP>      
      <p>Nazwa: {nazwaTowaru}</p>
      <p>Nazwa: {idTowaru}</p>
    </DivApp>
  );
}

export default App;
