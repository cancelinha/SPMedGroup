import React, { Component } from "react";
import Axios from "axios";
import { Link } from "react-router-dom";
import { parseJwt } from "../../services/auth";
import "../../assets/css/Con_Cadastro.css"
import "../../assets/css/Cads_Consulta.css"



class CadastroConsulta extends Component {
    constructor() {
        super();

        this.state = {
            idProntuario: "",
            idMedico: "",
            data: "",
            idStatus: "",
            descricao: "",
            medicos: [],
            prontuarios: []
        }

        this.atualizaridProntuario = this.atualizaridProntuario.bind(this);
        this.atualizaridMedico = this.atualizaridMedico.bind(this);
        this.atualizardata = this.atualizardata.bind(this);
        this.atualizaridStatus = this.atualizaridStatus.bind(this);
        this.atualizardescricao = this.atualizardescricao.bind(this);

    }

    //Pegar o valor que usuario digitar
    atualizaridProntuario(event) {
        this.setState({ idProntuario: event.target.value });
    }

    atualizaridMedico(event) {
        this.setState({ idMedico: event.target.value });
    }

    atualizardata(event) {
        this.setState({ data: event.target.value });
    }
    atualizaridStatus(event) {
        this.setState({ idStatus: event.target.value });
    }
    atualizardescricao(event) {
        this.setState({ descricao: event.target.value });
    }

    // lista todos os prontuarios
    listarProntuarios() {
        fetch('http://192.168.3.215:5000/api/Prontuario', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ prontuarios: data }))
            .catch(erro => console.log(erro))
    }

    //Cadastra o Prontuario com os dado passados pelo paciente
    cadastrarConsulta(event) {
        event.preventDefault();

        let prontuario = {
            idProntuario: this.state.idProntuario,
            idMedico: this.state.idMedico,
            data: this.state.data,
            idStatus: this.state.idStatus,
            descricao: this.state.descricao
        }
        // TODO arrumar daqui pra baixo
        console.log(prontuario)
         Axios({
            method: 'POST',
            url: 'http://192.168.3.215:5000/api/Consulta',
            data: prontuario,
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
        .then(data => {
            console.log(data.status)
            if(data.status == 200) {                
                if(parseJwt().TipoUsuario == "ADMINISTRADOR"){
                    this.props.history.push("/Home");
                } else if (parseJwt().TipoUsuario == "MEDICO"){
                    this.props.history.push("/MedicoConsultas");
                } else if (parseJwt().TipoUsuario == "PACIENTE"){
                    this.props.history.push("/PacienteConsultas");
                }
            };
        })
        .catch(erro => console.log(erro))

    }

    // lista todas os medicos
    listarMedicos() {
        fetch('http://192.168.3.215:5000/api/Medico', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ medicos: data }))
            .catch(erro => console.log(erro))
    }

    
    // carrega o metodo
    componentDidMount() {
        this.listarMedicos();
        this.listarProntuarios();
    }

    render() {
        return (
            <div className="All">
                <h1>SPMedicalGroup</h1>
                <h2>Cadastro de Consultas</h2>
                <h3>Preencha os campos abaixo para cadastrar uma nova consulta:</h3>
                <div className="inputs">

                    <form onSubmit={this.cadastrarConsulta.bind(this)}>
                        <select className="" type="text" required value={this.state.idProntuario} onChange={this.atualizaridProntuario}>
                        <option value="" selected disabled hidden>Selecione</option>
                            {
                                this.state.prontuarios.map((prontuario) => {
                                    return (
                                        <option key={prontuario.id} value={prontuario.id}>{prontuario.nome}</option>
                                    );
                                })
                            }
                        </select>
                        <select className="" type="text" required value={this.state.idMedico} onChange={this.atualizaridMedico}>
                        <option value="" selected disabled hidden>Selecione</option>
                            {
                               this.state.medicos.map((medico) => {
                                    return (
                                        <option key={medico.id} value={medico.id}>{medico.nome}</option>
                                    );
                                })
                            }
                        </select>
                        <input className="" type="date" placeholder="Data" value={this.state.data} onChange={this.atualizardata} />
                        <select className="" type="text" placeholder="Status" value={this.state.idStatus} onChange={this.atualizaridStatus} >
                            <option value="" selected disabled hidden>Selecione</option>
                            <option value="1" defaultValue>Agendada</option>
                            <option value="2" defaultValue>Realizada</option>
                            <option value="3" defaultValue>Cancelada</option>
                        </select>
                        <input className="" type="text" placeholder="Descricao" value={this.state.descricao} onChange={this.atualizardescricao} />


                        <button type="submit">Cadastrar</button>
                    </form>
                </div>

            </div >
        )
    }
}

export default CadastroConsulta;