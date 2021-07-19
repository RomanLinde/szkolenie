import { Button } from "@material-ui/core"

const ButtonP = (props:any) => {
    return (
      <Button variant="contained" color="primary" {...props}>
          {props.children}
      </Button>
    )
  }

  export default ButtonP