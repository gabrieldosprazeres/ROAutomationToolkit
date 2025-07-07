# Changelog

All notable changes to this project will be documented in this file.

## [1.1.0] - 07/07/2025

### Added

- Implementação do sistema de detecção de janelas do Ragexe
- Novo serviço `WindowEnumerator` para enumeração de processos
- Novo modelo `WindowInfo` para armazenar dados de janelas
- Interface de seleção de janelas com ComboBox e botão de refresh
- Operações assíncronas para carregamento de janelas
- Novo recurso gráfico (`refresh.png`) para o botão de atualização
- Tratamento de exceções durante coleta de informações de processos
- Suporte para ordenação de janelas por tempo de criação

### Changed

- **Refatoração completa da estrutura de pastas**:
  - Pasta `Forms` para componentes de UI
  - Pasta `Services` para lógica de negócios
  - Pasta `Models` para estruturas de dados
- Redimensionamento do formulário principal (350x300 → 375x300)
- Atualização do título da janela para "RO Automation Toolkit v1.1.0"
- Substituição de operações síncronas por versões assíncronas
- Otimização do processo de carregamento de janelas com threads separadas

### Fixed

- Tratamento robusto para processos inacessíveis
- Prevenção de vazamento de handles durante enumeração
- Ajuste no DPI scaling (PerMonitorV2)
- Corrigido comportamento do ComboBox com muitos itens

### Removed

- Removida inicialização redundante de componentes
- Eliminado código duplicado na disposição de controles

## [1.0.0] - 06/07/2025

### Added

- Release inicial do RO Automation Toolkit
- Formulário principal com layout básico
- Sistema de ícones e manifest
- Configuração básica de projeto Windows Forms
- Suporte inicial para HighDPI
