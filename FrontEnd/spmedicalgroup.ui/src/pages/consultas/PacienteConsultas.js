import React, { Component } from "react";
import { Link } from "react-router-dom";
import { logout } from "../../services/logout";

class PacienteConsultas extends Component {
    constructor() {
        super();

        this.state = {
            consultas: [],
            descricao: "",
            idDescricaoIncluir: ""
        }

        this.atualizarDescricao = this.atualizarDescricao.bind(this);
        this.atualizarIdDescricaoIncluir = this.atualizarIdDescricaoIncluir.bind(this);
    }

    logout() {
        localStorage.removeItem("userautent-token-spmedicalgroup");
    }
    // lista todas as consultas
    listarConsultas() {
        fetch('http://192.168.3.215:5000/api/Consulta', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ consultas: data }))
            // .then(data => {
            //     if (data.status == 200) {
            //         this.setState({ consultas: data })
            //     }
            // })
            .catch(erro => console.log(erro))
    }


    // carrega o metodo
    componentDidMount() {
        this.listarConsultas();
    }

    // pega a descriçao digitada
    atualizarDescricao(event) {
        this.setState({ descricao: event.target.value });
    }

    atualizarIdDescricaoIncluir(event) {
        this.setState({ idDescricaoIncluir: event.target.value });
    }

    render() {
        return (
            <div>
                <table>
                    <tbody> 
                        <tr>
                            <th>Id</th>
                            <th>IdProntuario</th>
                            <th>IdMedico</th>
                            <th>Data</th>
                            <th>IdStatus</th>
                            <th>Descricao</th>
                        </tr>

                        {
                            this.state.consultas.map(consulta => {
                                return (
                                    <tr key={consulta.id}>
                                        <td>{consulta.id}</td>
                                        <td>{consulta.idProntuario}</td>
                                        <td>{consulta.idMedico}</td>
                                        <td>{consulta.data.replace("T", " ").split(".")[0]}</td>
                                        <td>{consulta.idStatus}</td>
                                        <td>{consulta.descricao}</td>
                                    </tr>
                                );
                            })
                        }
                    </tbody>
                </table>
                <Link to="/">
                        <button onClick={this.logout}>Sair</button>
                </Link>
            </div>
        );
    }

}

export default PacienteConsultas;