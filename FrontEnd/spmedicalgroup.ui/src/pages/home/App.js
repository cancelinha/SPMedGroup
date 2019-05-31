import React, { Component } from "react";
import "../home/App.css"
class App extends Component{
    constructor(){
        super();
        
    }
    render(){
        return ( 
            <div className="container">
            <h1>SPMedicalGroup</h1>
            {/* <h2>Home</h2> */}
            <h3>Seja Bem-Vindo Administrador</h3>
            <p>O que dejesa executar:</p>
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