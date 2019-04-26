import React, { Component } from "react";
import Axios from "axios";
import { Link } from "react-router-dom";

class CadastroConsulta extends Component {
    constructor() {
        super();

        this.state = {
            idProntuario: "",
            idMedico: "",
            data: "",
            idStatus: "",
            descricao: "",

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


    //Cadastra o Prontuario com os dado passados pelo paciente
    cadastrarConsulta(event) {
        event.preventDefault();

        let prontuario = {
            idProntuario: this.state.idProntuario,
            idMedico: this.state.idMedico,
            data: this.state.data,
            idStatus: this.state.idStatus,
            descricao: this.state.descricao,


        }
        // TODO arrumar daqui pra baixo
        // console.log(prontuario)
        Axios({
            method: 'POST',
            url: 'http://localhost:5000/api/Consulta',
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
            <div>
                <form onSubmit={this.cadastrarConsulta.bind(this)}>
                    <input type="text" placeholder="IdProntuario" value={this.state.idProntuario} onChange={this.atualizaridProntuario} />
                    <input type="text" placeholder="IdMedico" value={this.state.idMedico} onChange={this.atualizaridMedico} />
                    <input type="text" placeholder="Data" value={this.state.data} onChange={this.atualizardata} />
                    <input type="text" placeholder="IdStatus" value={this.state.idStatus} onChange={this.atualizaridStatus} />
                    <input type="text" placeholder="Descricao" value={this.state.descricao} onChange={this.atualizardescricao} />


                    <button type="submit">Cadastrar</button>
                </form>
            </div>
        )
    }
}

export default CadastroConsulta;