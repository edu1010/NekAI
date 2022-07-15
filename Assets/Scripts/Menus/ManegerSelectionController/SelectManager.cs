using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;

public class SelectManager : MonoBehaviour, InputActions.IMenuActionsActions
{
    public static GameObject[] rows;
    [SerializeField]
    public List<GameObject> rowList = new List<GameObject>();
    [SerializeField]
    static int index = 0;
    public CanvasG ActualCanvasG = CanvasG.canvasgrup1;
    int limiteCanvas1;
    int limiteCanvas2;
    static GameObject previusSelected;
    Row currentRow;
    ISlider currentSlider;
    bool incrementSlider, decrementSlider;

    public static SelectManager Instance;
    // public static Image[] imagenes;

    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (rowList.Count > 0)
        {
            //EmptyList();
        }
        //fillList();
        DesactiveAllExcetTheChosenOne();
        obtenerLimites();
    }
    private void Update()
    {
        if (currentSlider == null) return;
        if (incrementSlider)
        {
            currentSlider.IncrementSlider();
        }else if (decrementSlider)
        {
            currentSlider.DecrementSlider();
        }
    }

    public void fillList()
    {
        rows = GameObject.FindGameObjectsWithTag("row");
        for (int i = 0; i < rows.Length; i++)
        {
            //            Debug.Log(rows.Length);
            rowList.Add(rows[i]);
        }
        rowList.OrderByDescending(x => x.GetComponent<Row>().canvasGroup).OrderByDescending(x => x.GetComponent<Row>().position);

    }
    public void EmptyList()
    {
        rowList.Clear();
    }
    public void DesactiveAllExcetTheChosenOne()
    {
        foreach (var row in rowList)
        {
            var cRow = row.GetComponent<Row>();
            Image image = row.GetComponentInChildren<Image>();
            if (cRow.selected)
            {
                previusSelected = cRow.gameObject;
                currentSlider = previusSelected.GetComponent<ISlider>();
                currentRow = cRow;
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
        }
    }
    public void DesactiveAll()
    {
        foreach (var row in rowList)
        {
            var cRow = row.GetComponent<Row>();
            Image image = row.GetComponentInChildren<Image>();
            cRow.selected = false;
            image.enabled = false;
        }

    }

    void obtenerLimites()//Limites para que al bajar se posicione el cursor en la primera parte del menu de arriba
    {
        limiteCanvas1 = rowList.Count(x => x.GetComponent<Row>().canvasGroup == CanvasG.canvasgrup1) - 1;
        limiteCanvas2 = rowList.Count(x => x.GetComponent<Row>().canvasGroup == CanvasG.canvasgrup2);
        limiteCanvas2 += limiteCanvas1;
    }

    public void ChangeMenu(int indexDeMenu)
    {
        index = indexDeMenu;
        DesactiveAll();
        rowList[index].GetComponent<Row>().selected = true;
        DesactiveAllExcetTheChosenOne();

    }

    public void OnSelectRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentRow.isLang)
            {
                currentSlider?.IncrementSlider();
            }
            else
            {
                incrementSlider = true;
            }
        }else if (context.canceled)
        {
            incrementSlider = false;
        }
        
        
    }

    public void OnSelectLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentRow.isLang)
            {
                currentSlider?.DecrementSlider();
            }
            else
            {
                decrementSlider = true;
            }
        }
        else if (context.canceled)
        {
            decrementSlider = false;
        }
    }

    public void OnMenuSelect(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        previusSelected.GetComponent<IRow>().ToDo();
    }

    public void OnMenuUp(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        index -= 1;
        index = changeIndex(index);
        previusSelected.GetComponent<Row>().selected = false;
        rowList[index].GetComponent<Row>().selected = true;
        DesactiveAllExcetTheChosenOne();
    }

    public void OnMenuDown(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        index += 1;
        Debug.Log(index + "bajar");
        index = changeIndex(index);
        previusSelected.GetComponent<Row>().selected = false;
        rowList[index].GetComponent<Row>().selected = true;
        DesactiveAllExcetTheChosenOne();
    }
    int changeIndex(int _index)//Comprovamos que el valor existe y tiene coherencia
    {
        if (ActualCanvasG == CanvasG.canvasgrup1)
        {
            if (_index > limiteCanvas1)
            {
                _index = 0;
            }
            if (_index < 0)
            {
                _index = limiteCanvas1;
            }
        }
        else
        {
            if (_index > limiteCanvas2)
            {
                _index = limiteCanvas1 + 1;
                Debug.Log("vuleta canvas 2 " + _index);
            }
            if (_index <= limiteCanvas1)
            {
                _index = limiteCanvas2;
                Debug.Log("vuleta canvas inversa 2  " + _index);
            }
        }
        return _index;
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("pause");
        GameFlowController.Instance.PauseGame();
    }
}
