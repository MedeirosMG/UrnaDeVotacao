var app = angular.module('UrnaVotacao', []);

app.controller('UrnaController', ['$scope', function ($scope) {
    var custonAlert = new CustomAlert();
    $scope.tipoCandidato;
    $scope.tipoCandidatoS;
    $scope.nomeCandidato;
    $scope.finalVotacao;
    $scope.TipoCandidatoSelecionado;
    $scope.idCandidatoSelecionado;

    $scope.votosComputados = new Array();
    $scope.CandidatosAdicionados = new Array();
    $scope.caminhoImagem = "";

    $scope.modeloCandidato = { codigo: "" };

    $scope.carregaNome = function (candidatoNome) {
        $scope.nomeCandidato = candidatoNome;

        if (!$scope.$$phase) {
            $scope.$apply();
        }
    }

    $scope.carregaFotoCandidato = function (candidatoFoto) {

        if (candidatoFoto == "branco") {
            $scope.nomeCandidato = "Branco";
            $scope.caminhoImagem = "Voto_Branco.jpeg";
        } else if (candidatoFoto == "nulo") {
            $scope.nomeCandidato = "Nulo";
            $scope.caminhoImagem = "Voto_Nulo.jpeg";
        } else if (candidatoFoto != "") {
            $scope.caminhoImagem = $scope.idCandidatoSelecionado + ".jpeg";
            $scope.nomeCandidato = candidatoFoto;
        } else {
            $scope.caminhoImagem = "vazio.JPG";
            $scope.nomeCandidato = candidatoFoto;
        }

        if(!$scope.$$phase) {
            $scope.$apply();
        }
    }

    $scope.VotoBranco = function () {
        $scope.TipoCandidatoSelecionado = $scope.tipoCandidato;
        $scope.idCandidatoSelecionado = -2;

        $scope.carregaNome("branco");
        $scope.carregaFotoCandidato("branco");
        $scope.modeloCandidato.codigo = "";
    };

    $scope.VotoNulo = function () {
        $scope.TipoCandidatoSelecionado = $scope.tipoCandidato;
        $scope.idCandidatoSelecionado = -1;

        $scope.carregaNome("Nulo");
        $scope.carregaFotoCandidato("nulo");
        $scope.modeloCandidato.codigo = "";
    };

    $scope.armanezaVoto = function () {
        var candidato = { IdCandidato: $scope.idCandidatoSelecionado, tipoCandidato: $scope.TipoCandidatoSelecionado };
        $scope.votosComputados.push(candidato);

        var candidatoTotal = { NomeCandidato: $scope.nomeCandidato, fotoCandidato: $scope.caminhoImagem};
        $scope.CandidatosAdicionados.push(candidatoTotal);

;        if ($scope.tipoCandidato < 3) {    
            $scope.modeloCandidato.codigo = "";
            $scope.tipoCandidato++;
            $scope.getTipoEleitor();
        } else {            
            $scope.tipoCandidato = -1;
        }

        $scope.carregaNome("");
        $scope.carregaFotoCandidato("");
    }

    $scope.defineVotacao = function(voto){
        $scope.finalVotacao = voto;
    }

    $scope.confirmaVotos = function () {
        votos = $scope.votosComputados
        for (i = 0; i < 3; i++) {
            $.ajax({
                type: "GET",
                url: "/ProjetoEleicao.Web/Eleitor/Votar",
                contentType: "application/json; charset=utf-8",
                data: { idCandidato: votos[i].IdCandidato, tipoCandidato: votos[i].tipoCandidato },
                success: function (data) {
                    if ((data.Message).indexOf("sucesso") >= 0) {
                    } else {
                    }
                },
                error: function () {
                }
            });
        }
                
        custonAlert.alertSuccess("Votação realizada", "Votação realizada com sucesso!!");
        $scope.tipoCandidatoS = "";

        $("#ModalVotacao").modal("hide");
        $scope.votosComputados = "";
    };

    
    $scope.carregaCandidato = function () {
        if ($scope.modeloCandidato.codigo != "") {
            $.ajax({
                type: "GET",
                url: "/ProjetoEleicao.Web/Candidato/GetCandidatoByCode",
                contentType: "application/json; charset=utf-8",
                data: { code: $scope.modeloCandidato.codigo },
                success: function (data) {
                    $scope.TipoCandidatoSelecionado = $scope.tipoCandidato;                    
                    if (data.Content.IdTipoCandidato == $scope.tipoCandidato && !((data.Content.Nome).indexOf("Branco") >= 0) && !((data.Content.Nome).indexOf("Nulo") >= 0)) {
                        $scope.idCandidatoSelecionado = data.Content.IdCandidato;
                        $scope.carregaFotoCandidato(data.Content.Nome);
                        $scope.carregaNome(data.Content.Nome);
                    } else {
                        $scope.idCandidatoSelecionado = -1;
                        $scope.carregaFotoCandidato("nulo");
                        $scope.carregaNome("Nulo");
                    }
                },
                error: function () {
                    $scope.TipoCandidatoSelecionado = $scope.tipoCandidato;
                    $scope.idCandidatoSelecionado = -1;
                    $scope.carregaFotoCandidato("nulo");
                    $scope.carregaNome("Nulo");
                }
            });
        }
    };

    $scope.getTipoEleitor = function () {
        $.ajax({
            type: "GET",
            url: "/ProjetoEleicao.Web/TipoCandidato/GetTipoCandidatoByCode",
            contentType: "application/json; charset=utf-8",
            data: { code: $scope.tipoCandidato },
            success: function (data) {
                $scope.tipoCandidatoS = data.Content.Descricao;
                $scope.$apply();
            },
            error: function(){}
        });
    }

    $scope.IniciarVotacao = function () {
        if ($scope.SenhaAdministrador == "senhaadministrador") {
            $.ajax({
                type: "GET",
                url: "/ProjetoEleicao.Web/Uev/iniciavotacao",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                    if (data.Success) {
                        $("#LoginOperador").modal("hide");
                        custonAlert.alertSuccess("Votação iniciada", "Votação iniciada com sucesso!!");
                    } else {
                        custonAlert.alertError("Erro", "Erro ao iniciar a votação.");
                    }
                }
            });
        } else {
            custonAlert.alertError("Senha", "Senha de administrador incorreta.");
        }
    };

    $scope.FinalizaVotacao = function () {
        if ($scope.SenhaAdministrador == "senhaadministrador") {
            $.ajax({
                type: "POST",
                url: "/ProjetoEleicao.Web/Uev/RegistrarDadosUeg",
                contentType: "application/json; charset=utf-8",
                success: function (data) {                   
                    custonAlert.alertSuccess("Votação finalizada", "Votação finalizada com sucesso!!");
                },
                error: function (data) {
                    custonAlert.alertError("Erro", "Erro ao iniciar a votação.");
                }
            });
        } else {
            custonAlert.alertError("Senha", "Senha de administrador incorreta.");
        }
    };

    $scope.ButtonLogin = function () {          
        $.ajax({
            type: "GET",
            url: "/ProjetoEleicao.web/Eleitor/GetEleitorByCode",
            data: { cpf: $scope.eleitorCpf },
            success: function (eleitor) {
                $("#ModalVotacao").modal("show");
                $scope.CpfEleitor = $scope.eleitorCpf;
                $scope.tipoCandidato = 1;
                $scope.getTipoEleitor();
            },
            error: function () {
                custonAlert.alertError("Usuario não encontrado", "Cpf do usuario digitado não cadastrado no sistema.");
            }
        });
    };
}]);