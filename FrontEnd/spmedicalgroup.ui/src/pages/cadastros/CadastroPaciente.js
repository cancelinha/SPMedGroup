import React, { Component } from "react";
import Axios from "axios";
import { Link } from "react-router-dom";
import "../../assets/css/Cads_Paciente.css"



class CadastroPaciente extends Component {
    constructor() {
        super();

        this.state = {
            nome: "",
            cpf: "",
            rg: "",
            endereco: "",
            idUsuario: "",

        }

        this.atualizarnome = this.atualizarnome.bind(this);
        this.atualizarcpf = this.atualizarcpf.bind(this);
        this.atualizarrg = this.atualizarrg.bind(this);
        this.atualizarendereco = this.atualizarendereco.bind(this);
        this.atualizaridUsuario = this.atualizaridUsuario.bind(this);

    }

    //Pegar o valor que usuario digitar
    atualizarnome(event) {
        this.setState({ nome: event.target.value });
    }

    atualizarcpf(event) {
        this.setState({ cpf: event.target.value });
    }

    atualizarrg(event) {
        this.setState({ rg: event.target.value });
    }
    atualizarendereco(event) {
        this.setState({ endereco: event.target.value });
    }
    atualizaridUsuario(event) {
        this.setState({ idUsuario: event.target.value });
    }

    
    //Cadastra o Prontuario com os dado passados pelo paciente
    cadastrarPaciente(event) {
        event.preventDefault();

        let prontuario = {
            nome: this.state.nome,
            cpf: this.state.cpf,
            rg: this.state.rg,
            endereco: this.state.endereco,
            idUsuario: this.state.idUsuario,


        }
        // TODO arrumar daqui pra baixo
        // console.log(prontuario)
        Axios({
            method: 'POST',
            url: 'http://192.168.3.215:5000/api/Prontuario',
            data: prontuario,
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        }).catch(erro => console.log(erro))

    }
    // Axios({method: 'POST',
    // url: 'http://localhost:5000/api/Prontuario',
    // data: {prontuario},
    // headers: {
    //  "Content-Type": "application/json",
    //  "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
    // }
    // }).catch(erro => console.log(erro))}
    //     Axios.post('http://localhost:5000/api/Prontuario', {
    //         method: "POST",
    //         data: {prontuario : this.state.prontuario},
    //         headers: {
    //             "Content-Type": "application/json",
    //             "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")

    //         }
    //     })
    //     .catch(erro => console.log(erro))
    // }

    render() {
        return (
            <div className="up">
                 <h1>SPMedicalGroup</h1>
                 <h2>Cadastro de Pacientes</h2>
                <h3>Preencha os campos abaixo para cadastrar um novo Paciente:</h3>
                <form onSubmit={this.cadastrarPaciente.bind(this)}>
                <div className="inputs">
                    <input type="text" placeholder="Nome" value={this.state.nome} onChange={this.atualizarnome} />
                    <input type="text" placeholder="CPF" value={this.state.cpf} onChange={this.atualizarcpf} />
                    <input type="text" placeholder="RG" value={this.state.rg} onChange={this.atualizarrg} />
                    <input type="text" placeholder="Endereço" value={this.state.endereco} onChange={this.atualizarendereco} />
                    <input type="text" placeholder="IdUsuario" value={this.state.idUsuario} onChange={this.atualizaridUsuario} />


                    <button type="submit">Cadastrar</button>
                    </div>
                </form>
            </div>
        )
    }
}

export default CadastroPaciente;