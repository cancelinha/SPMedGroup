import React, { Component } from "react";

class App extends Component{
    constructor(){
        super();
        
    }
    render(){
        return ( 
            <div className="container">
            <h1>SPMedicalGroup</h1>
            <h2>Home</h2>
            <div className="Linkagem">
            <a href="/ListagemUsuarios">Listar os Usuarios </a>
            <a href="/ListagemConsultas">Listar as Consultas</a>
            <a href="/CadastroPaciente">Cadastrar Novo Paciente</a>
            <a href="/CadastroConsulta">Cadastrar Nova Consulta</a>
            </div>
            </div>
        
        )
    }
}
export default App;