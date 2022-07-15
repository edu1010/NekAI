// GENERATED AUTOMATICALLY FROM 'Assets/Input_Action_Maps/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""InGameActions"",
            ""id"": ""658ca3df-13d0-484d-a7ae-e434c41a1f5a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d2aee0c8-d171-4ff1-a920-15c4ecf8c379"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""eb6be03f-4786-458d-b090-1f1e73a0ba0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""2c76e137-8f75-46a5-a4f0-9e0f5b0ef5ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""443fcf8a-61b5-48eb-b45f-b0e1d7a64cef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""7d2c230d-78a6-4ec8-ab84-faa1d46819c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""0f6b8da9-7498-4ab5-9190-04e7df18bcac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heal"",
                    ""type"": ""Button"",
                    ""id"": ""ed26e389-26ae-4071-9d0e-75fdab6b59de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""NextText"",
                    ""type"": ""Button"",
                    ""id"": ""26b34e57-ec74-448e-be64-7406f5845205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Joystick"",
                    ""id"": ""eb68be93-914c-4c28-8d63-421dfe0eca64"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c4a07f40-cd67-401c-a648-ab1097812890"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c1937424-f770-4799-81a9-2d42f499c96f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e3a1a8cc-e8bd-41b3-bf54-63434f980894"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""297fd8bb-1b56-460a-b61a-fad6bb9a6be8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""7f69ab52-ea6d-4c30-b23b-60a633c33a87"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""62793ef7-9178-4cfe-9bd4-b540251ccc9a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""20bb3552-fadb-4de6-8243-2563edbd2d91"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9e3f3795-5ce3-4f8a-996f-91c133b5d2e7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""98f6c6f3-3d6a-440d-853e-a07643a5feca"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bf32403f-3068-4699-b5c4-99bef094d68f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6aea5a55-c878-47b9-9604-68480393b90f"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f18c175-0761-4ee7-86c2-e0e7aabc8ce9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6caf929-1ba8-4c6c-a370-1df1e52acfd8"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0856a4a5-923f-4a2a-9aa3-d5625ff4cd7d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e2e7b47-da24-4a74-a0a6-e5c175e3bd75"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78523a02-3139-4488-b066-baa8f3234f1a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e67a463-a78b-460e-b514-a2233d0d732d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""673d7fa0-c0c1-4b40-a734-2cf1fbb692e2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f636ad3-6738-4c97-a959-ddf1eaf6c6a5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af565ca7-b1d2-43f7-b1dc-fbd0ed30f5f4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6aa5492-ade5-4fc5-9239-19c4b45fe6a0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58208726-8408-49ef-b17e-899752c07601"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""NextText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2888dce9-488c-47c0-a934-77b65e85a017"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NextText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11cee4c2-ff75-4e51-9f97-2271a0051177"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NextText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fddf8cf9-13ae-4c1e-84c9-e5cce0d43c3e"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NextText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35c5298f-ae69-445e-88a0-80f65faa38c3"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NextText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuActions"",
            ""id"": ""125436cb-a302-4000-a999-4661657cb1f7"",
            ""actions"": [
                {
                    ""name"": ""SelectRight"",
                    ""type"": ""Button"",
                    ""id"": ""6b12f98a-e36e-4f24-933a-628259599e74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectLeft"",
                    ""type"": ""Button"",
                    ""id"": ""075ff605-5f39-43f1-a16d-c32425b8f45a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuSelect"",
                    ""type"": ""Button"",
                    ""id"": ""14f071a9-a14c-41ec-9ad6-8574d05b6c8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuUp"",
                    ""type"": ""Button"",
                    ""id"": ""63ba4f52-c46b-4c13-8d09-aff355fdf5fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuDown"",
                    ""type"": ""Button"",
                    ""id"": ""07d2ce9f-e83f-46d3-86ec-32e611b0d085"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""7c741526-dd73-49d9-a881-ecb83892f76d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f6c2734a-6f18-4f61-a5f9-83be3cf1b6b7"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""762cfb8e-082f-4899-af94-70bc1c3867ec"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f5761c0-34cc-43f1-896f-e340aa109b2c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c3bfc6f-5a0c-484b-9502-be78ee6f506b"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MenuSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1af5f17-59a3-41f2-b929-ee577b7f3b22"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9706670-879d-45cd-a798-8b1ab7c8eeb5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""830b2ba7-be9f-484f-91a4-449086282a86"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96ae7fb5-ea2d-4b1b-9b66-8fb05a4a0e45"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MenuUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5baaed16-224d-4fde-a7de-f2b59c0738ca"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4165aae-b0ba-45d8-a8c6-ecad042f8238"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dae14a8-75a7-45fc-9385-65d5e27a3038"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MenuDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bfad6d2-ecae-4594-bbdf-dee13feb614e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""825aeb46-8eed-4ed6-946c-a79ad125d673"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuMap"",
            ""id"": ""7b34e29c-64d6-4cc7-9231-34a973bbbddd"",
            ""actions"": [
                {
                    ""name"": ""SelectRight"",
                    ""type"": ""Button"",
                    ""id"": ""529dd1a3-aaa0-46bc-b659-817a42b443ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuUp"",
                    ""type"": ""Button"",
                    ""id"": ""931b43ad-b6e1-4ea0-b886-542c7fb85360"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuDown"",
                    ""type"": ""Button"",
                    ""id"": ""0be71dce-3cb7-46d0-88ea-dca99fe88af6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuSelect"",
                    ""type"": ""Button"",
                    ""id"": ""10d157fa-299f-4827-afba-ab3b5daa1328"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectLeft"",
                    ""type"": ""Button"",
                    ""id"": ""f4c6da85-b10e-4098-8951-ef2b40da0c32"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a7b4ed02-9de4-4cdc-aafc-7e16328a7129"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31fac12a-2501-4fbe-b23f-253d43ce940d"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MenuSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8872daec-6da1-432d-9764-dffa4577248a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91987bac-55db-4b76-a466-b8a0076a7265"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eb3993a-50cf-44fa-be79-2c362ad538c4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c84e81cc-8440-4d04-8f23-7945cdb41358"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08373c4b-231d-42ad-baaf-1f03d4b9e683"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e0b5e3e-213e-4a75-baff-209960c26706"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6670d31c-a353-4cae-a3c4-c2d9750b7297"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // InGameActions
        m_InGameActions = asset.FindActionMap("InGameActions", throwIfNotFound: true);
        m_InGameActions_Move = m_InGameActions.FindAction("Move", throwIfNotFound: true);
        m_InGameActions_Jump = m_InGameActions.FindAction("Jump", throwIfNotFound: true);
        m_InGameActions_Dash = m_InGameActions.FindAction("Dash", throwIfNotFound: true);
        m_InGameActions_Attack = m_InGameActions.FindAction("Attack", throwIfNotFound: true);
        m_InGameActions_Shoot = m_InGameActions.FindAction("Shoot", throwIfNotFound: true);
        m_InGameActions_Pause = m_InGameActions.FindAction("Pause", throwIfNotFound: true);
        m_InGameActions_Heal = m_InGameActions.FindAction("Heal", throwIfNotFound: true);
        m_InGameActions_NextText = m_InGameActions.FindAction("NextText", throwIfNotFound: true);
        // MenuActions
        m_MenuActions = asset.FindActionMap("MenuActions", throwIfNotFound: true);
        m_MenuActions_SelectRight = m_MenuActions.FindAction("SelectRight", throwIfNotFound: true);
        m_MenuActions_SelectLeft = m_MenuActions.FindAction("SelectLeft", throwIfNotFound: true);
        m_MenuActions_MenuSelect = m_MenuActions.FindAction("MenuSelect", throwIfNotFound: true);
        m_MenuActions_MenuUp = m_MenuActions.FindAction("MenuUp", throwIfNotFound: true);
        m_MenuActions_MenuDown = m_MenuActions.FindAction("MenuDown", throwIfNotFound: true);
        m_MenuActions_Pause = m_MenuActions.FindAction("Pause", throwIfNotFound: true);
        // MenuMap
        m_MenuMap = asset.FindActionMap("MenuMap", throwIfNotFound: true);
        m_MenuMap_SelectRight = m_MenuMap.FindAction("SelectRight", throwIfNotFound: true);
        m_MenuMap_MenuUp = m_MenuMap.FindAction("MenuUp", throwIfNotFound: true);
        m_MenuMap_MenuDown = m_MenuMap.FindAction("MenuDown", throwIfNotFound: true);
        m_MenuMap_MenuSelect = m_MenuMap.FindAction("MenuSelect", throwIfNotFound: true);
        m_MenuMap_SelectLeft = m_MenuMap.FindAction("SelectLeft", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // InGameActions
    private readonly InputActionMap m_InGameActions;
    private IInGameActionsActions m_InGameActionsActionsCallbackInterface;
    private readonly InputAction m_InGameActions_Move;
    private readonly InputAction m_InGameActions_Jump;
    private readonly InputAction m_InGameActions_Dash;
    private readonly InputAction m_InGameActions_Attack;
    private readonly InputAction m_InGameActions_Shoot;
    private readonly InputAction m_InGameActions_Pause;
    private readonly InputAction m_InGameActions_Heal;
    private readonly InputAction m_InGameActions_NextText;
    public struct InGameActionsActions
    {
        private @InputActions m_Wrapper;
        public InGameActionsActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_InGameActions_Move;
        public InputAction @Jump => m_Wrapper.m_InGameActions_Jump;
        public InputAction @Dash => m_Wrapper.m_InGameActions_Dash;
        public InputAction @Attack => m_Wrapper.m_InGameActions_Attack;
        public InputAction @Shoot => m_Wrapper.m_InGameActions_Shoot;
        public InputAction @Pause => m_Wrapper.m_InGameActions_Pause;
        public InputAction @Heal => m_Wrapper.m_InGameActions_Heal;
        public InputAction @NextText => m_Wrapper.m_InGameActions_NextText;
        public InputActionMap Get() { return m_Wrapper.m_InGameActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActionsActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActionsActions instance)
        {
            if (m_Wrapper.m_InGameActionsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnDash;
                @Attack.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnAttack;
                @Shoot.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnShoot;
                @Pause.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnPause;
                @Heal.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnHeal;
                @NextText.started -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnNextText;
                @NextText.performed -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnNextText;
                @NextText.canceled -= m_Wrapper.m_InGameActionsActionsCallbackInterface.OnNextText;
            }
            m_Wrapper.m_InGameActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
                @NextText.started += instance.OnNextText;
                @NextText.performed += instance.OnNextText;
                @NextText.canceled += instance.OnNextText;
            }
        }
    }
    public InGameActionsActions @InGameActions => new InGameActionsActions(this);

    // MenuActions
    private readonly InputActionMap m_MenuActions;
    private IMenuActionsActions m_MenuActionsActionsCallbackInterface;
    private readonly InputAction m_MenuActions_SelectRight;
    private readonly InputAction m_MenuActions_SelectLeft;
    private readonly InputAction m_MenuActions_MenuSelect;
    private readonly InputAction m_MenuActions_MenuUp;
    private readonly InputAction m_MenuActions_MenuDown;
    private readonly InputAction m_MenuActions_Pause;
    public struct MenuActionsActions
    {
        private @InputActions m_Wrapper;
        public MenuActionsActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectRight => m_Wrapper.m_MenuActions_SelectRight;
        public InputAction @SelectLeft => m_Wrapper.m_MenuActions_SelectLeft;
        public InputAction @MenuSelect => m_Wrapper.m_MenuActions_MenuSelect;
        public InputAction @MenuUp => m_Wrapper.m_MenuActions_MenuUp;
        public InputAction @MenuDown => m_Wrapper.m_MenuActions_MenuDown;
        public InputAction @Pause => m_Wrapper.m_MenuActions_Pause;
        public InputActionMap Get() { return m_Wrapper.m_MenuActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActionsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActionsActions instance)
        {
            if (m_Wrapper.m_MenuActionsActionsCallbackInterface != null)
            {
                @SelectRight.started -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnSelectRight;
                @SelectRight.performed -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnSelectRight;
                @SelectRight.canceled -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnSelectRight;
                @SelectLeft.started -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.performed -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.canceled -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnSelectLeft;
                @MenuSelect.started -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuSelect;
                @MenuSelect.performed -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuSelect;
                @MenuSelect.canceled -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuSelect;
                @MenuUp.started -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuUp;
                @MenuUp.performed -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuUp;
                @MenuUp.canceled -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuUp;
                @MenuDown.started -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuDown;
                @MenuDown.performed -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuDown;
                @MenuDown.canceled -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnMenuDown;
                @Pause.started -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuActionsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MenuActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectRight.started += instance.OnSelectRight;
                @SelectRight.performed += instance.OnSelectRight;
                @SelectRight.canceled += instance.OnSelectRight;
                @SelectLeft.started += instance.OnSelectLeft;
                @SelectLeft.performed += instance.OnSelectLeft;
                @SelectLeft.canceled += instance.OnSelectLeft;
                @MenuSelect.started += instance.OnMenuSelect;
                @MenuSelect.performed += instance.OnMenuSelect;
                @MenuSelect.canceled += instance.OnMenuSelect;
                @MenuUp.started += instance.OnMenuUp;
                @MenuUp.performed += instance.OnMenuUp;
                @MenuUp.canceled += instance.OnMenuUp;
                @MenuDown.started += instance.OnMenuDown;
                @MenuDown.performed += instance.OnMenuDown;
                @MenuDown.canceled += instance.OnMenuDown;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MenuActionsActions @MenuActions => new MenuActionsActions(this);

    // MenuMap
    private readonly InputActionMap m_MenuMap;
    private IMenuMapActions m_MenuMapActionsCallbackInterface;
    private readonly InputAction m_MenuMap_SelectRight;
    private readonly InputAction m_MenuMap_MenuUp;
    private readonly InputAction m_MenuMap_MenuDown;
    private readonly InputAction m_MenuMap_MenuSelect;
    private readonly InputAction m_MenuMap_SelectLeft;
    public struct MenuMapActions
    {
        private @InputActions m_Wrapper;
        public MenuMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectRight => m_Wrapper.m_MenuMap_SelectRight;
        public InputAction @MenuUp => m_Wrapper.m_MenuMap_MenuUp;
        public InputAction @MenuDown => m_Wrapper.m_MenuMap_MenuDown;
        public InputAction @MenuSelect => m_Wrapper.m_MenuMap_MenuSelect;
        public InputAction @SelectLeft => m_Wrapper.m_MenuMap_SelectLeft;
        public InputActionMap Get() { return m_Wrapper.m_MenuMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuMapActions set) { return set.Get(); }
        public void SetCallbacks(IMenuMapActions instance)
        {
            if (m_Wrapper.m_MenuMapActionsCallbackInterface != null)
            {
                @SelectRight.started -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnSelectRight;
                @SelectRight.performed -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnSelectRight;
                @SelectRight.canceled -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnSelectRight;
                @MenuUp.started -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuUp;
                @MenuUp.performed -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuUp;
                @MenuUp.canceled -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuUp;
                @MenuDown.started -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuDown;
                @MenuDown.performed -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuDown;
                @MenuDown.canceled -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuDown;
                @MenuSelect.started -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuSelect;
                @MenuSelect.performed -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuSelect;
                @MenuSelect.canceled -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnMenuSelect;
                @SelectLeft.started -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.performed -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.canceled -= m_Wrapper.m_MenuMapActionsCallbackInterface.OnSelectLeft;
            }
            m_Wrapper.m_MenuMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectRight.started += instance.OnSelectRight;
                @SelectRight.performed += instance.OnSelectRight;
                @SelectRight.canceled += instance.OnSelectRight;
                @MenuUp.started += instance.OnMenuUp;
                @MenuUp.performed += instance.OnMenuUp;
                @MenuUp.canceled += instance.OnMenuUp;
                @MenuDown.started += instance.OnMenuDown;
                @MenuDown.performed += instance.OnMenuDown;
                @MenuDown.canceled += instance.OnMenuDown;
                @MenuSelect.started += instance.OnMenuSelect;
                @MenuSelect.performed += instance.OnMenuSelect;
                @MenuSelect.canceled += instance.OnMenuSelect;
                @SelectLeft.started += instance.OnSelectLeft;
                @SelectLeft.performed += instance.OnSelectLeft;
                @SelectLeft.canceled += instance.OnSelectLeft;
            }
        }
    }
    public MenuMapActions @MenuMap => new MenuMapActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IInGameActionsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
        void OnNextText(InputAction.CallbackContext context);
    }
    public interface IMenuActionsActions
    {
        void OnSelectRight(InputAction.CallbackContext context);
        void OnSelectLeft(InputAction.CallbackContext context);
        void OnMenuSelect(InputAction.CallbackContext context);
        void OnMenuUp(InputAction.CallbackContext context);
        void OnMenuDown(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMenuMapActions
    {
        void OnSelectRight(InputAction.CallbackContext context);
        void OnMenuUp(InputAction.CallbackContext context);
        void OnMenuDown(InputAction.CallbackContext context);
        void OnMenuSelect(InputAction.CallbackContext context);
        void OnSelectLeft(InputAction.CallbackContext context);
    }
}
