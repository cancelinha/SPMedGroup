import React, { Component } from "react";
import {Link} from "react-router-dom";
import { logout } from "../../services/logout";

class ConsultasPaciente extends Component {
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

    // carrega o metodo
    componentDidMount() {
        this.listarConsultas();
    }

    // lista todas as consultas
    listarConsultas() {
        fetch('http://localhost:5000/api/Consultas', {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ consultas: data }))
            .catch(erro => console.log(erro))
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
                            <th>DataAgendada</th>
                            <th>HoraAgendade</th>
                            <th>IdSituacao</th>
                            <th>Descricao</th>
                        </tr>

                        {
                            this.state.consultas.map(consulta => {
                                return (
                                    <tr key={consulta.id}>
                                        <td>{consulta.id}</td>
                                        <td>{consulta.idProntuario}</td>
                                        <td>{consulta.idMedico}</td>
                                        <td>{consulta.dataAgendada.replace("T", " ").split(".")[0]}</td>
                                        <td>{consulta.horaAgendada}</td>
                                        <td>{consulta.idSituacao}</td>
                                        <td>{consulta.descricao}</td>
                                    </tr>
                                );
                            })
                        }
                    </tbody>
                </table>
                <Link onClick={logout}>Sair</Link>
            </div>
        );
    }
}

export default ConsultasPaciente;